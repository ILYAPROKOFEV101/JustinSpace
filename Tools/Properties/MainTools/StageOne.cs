
using System;
using Tools.Properties.MainTools;
using System.Linq; // Для работы .LastOrDefault()

public class StageOne : Stages
{
    
        public StageOne(double dryMass, double fuelMass, double fuelConsumptionRate)
        {
            this.dryMass = dryMass;
            this.fuelMass = fuelMass;
            this.fuelConsumptionRate = fuelConsumptionRate;

            t = 118.0;
            F = 4997904.0;
            Fmin = 4147360.0;
            angle = 61.0;

            CalculateTimeValues();
            CalculateFunction();
        }

        public void CalculateFunction()
        {
            fuelLeftValues.Clear();
    
            for (int i = 0; i < timeValues.Count; i++)
            {
                double time = timeValues[i];
                double currentMass = GetCurrentMass(time);

                if (currentMass <= dryMass + 0.001)
                {
                    // Ускорение (только гравитация)
                    accelerationXValues.Add(0);
                    accelerationYValues.Add(-g);
            
                    // Получаем последние значения скорости (без LINQ и ^1)
                    double lastSpeedX = speedXValues.Count > 0 ? speedXValues[speedXValues.Count - 1] : 0;
                    double lastSpeedY = speedYValues.Count > 0 ? speedYValues[speedYValues.Count - 1] : 0;
            
                    // Новая скорость с учетом гравитации
                    speedXValues.Add(lastSpeedX + 0 * step); // Ускорение X = 0
                    speedYValues.Add(lastSpeedY + (-g) * step); // Ускорение Y = -g
            
                    // Получаем последние позиции
                    double lastMoveX = movementXValues.Count > 0 ? movementXValues[movementXValues.Count - 1] : 0;
                    double lastMoveY = movementYValues.Count > 0 ? movementYValues[movementYValues.Count - 1] : 0;
            
                    // Новая позиция
                    movementXValues.Add(lastMoveX + speedXValues[speedXValues.Count - 1] * step);
                    movementYValues.Add(lastMoveY + speedYValues[speedYValues.Count - 1] * step);
            
                    continue;
                }

                accelerationXValues.Add(XFunction(time));
                accelerationYValues.Add(YFunction(time));
        
                speedXValues.Add(Euler(SetStartValues(speedXValues, null, i), accelerationXValues[i]));
                speedYValues.Add(Euler(SetStartValues(speedYValues, null, i), accelerationYValues[i]));
        
                movementXValues.Add(Euler(SetStartValues(movementXValues, null, i), speedXValues[i]));
                movementYValues.Add(Euler(SetStartValues(movementYValues, null, i), speedYValues[i]));
            }

            PrintParameters();
        }

        private double XFunction(double time)
        {
            // Если топливо закончилось - ускорение равно 0
            if (fuelMass - fuelConsumptionRate * time <= 0)
                return 0;
        
            double force = Fmin + EngineForceIncrease() * time;
            double angleRad = RotationAngleFunction() * time;
            double mass = GetCurrentMass(time);
            return (force * Math.Sin(angleRad)) / mass;
        }

        private double YFunction(double time)
        {
            // Если топливо закончилось - только гравитация
            if (fuelMass - fuelConsumptionRate * time <= 0)
                return -g;  // Только ускорение свободного падения
        
            double force = Fmin + EngineForceIncrease() * time;
            double angleRad = RotationAngleFunction() * time;
            double mass = GetCurrentMass(time);
            return (force * Math.Cos(angleRad)) / mass - g;
        }

        private double EngineForceIncrease()
        {
            return (F - Fmin) / t;
        }
    }
