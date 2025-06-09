
using System;
using Tools.Properties.MainTools;

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
            for (int i = 0; i < timeValues.Count; i++)
            {
                double time = timeValues[i];

                accelerationXValues.Add(XFunction(time));
                accelerationYValues.Add(YFunction(time));

                speedXValues.Add(Euler(SetStartValues(speedXValues, null, i), accelerationXValues[i]));
                speedYValues.Add(Euler(SetStartValues(speedYValues, null, i), accelerationYValues[i]));

                movementXValues.Add(Euler(SetStartValues(movementXValues, null, i), speedXValues[i]));
                movementYValues.Add(Euler(SetStartValues(movementYValues, null, i), speedYValues[i]));
            }

            PrintParameters();
        }

        private double XFunction(double arg)
        {
            double force = Fmin + EngineForceIncrease() * arg;
            double angleRad = RotationAngleFunction() * arg;
            double mass = GetCurrentMass(arg);
            return (force * Math.Sin(angleRad)) / mass;
        }

        private double YFunction(double arg)
        {
            double force = Fmin + EngineForceIncrease() * arg;
            double angleRad = RotationAngleFunction() * arg;
            double mass = GetCurrentMass(arg);
            return (force * Math.Cos(angleRad)) / mass - g;
        }

        private double EngineForceIncrease()
        {
            return (F - Fmin) / t;
        }
    }
