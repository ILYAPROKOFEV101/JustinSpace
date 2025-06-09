using System;

namespace Tools.Properties.MainTools
{
    public class StageThree : Stages
    {
        private readonly StageTwo stageTwo;

        public StageThree(StageTwo stageTwo, double dryMass, double fuelMass, double fuelConsumptionRate)
        {
            this.stageTwo = stageTwo;

            this.dryMass = dryMass;
            this.fuelMass = fuelMass;
            this.fuelConsumptionRate = fuelConsumptionRate;

            t = 224.0;
            F = 294000.0;
            angle = 8.0;

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

                speedXValues.Add(Euler(SetStartValues(speedXValues, stageTwo.SpeedXValues, i), accelerationXValues[i]));
                speedYValues.Add(Euler(SetStartValues(speedYValues, stageTwo.SpeedYValues, i), accelerationYValues[i]));

                movementXValues.Add(Euler(SetStartValues(movementXValues, stageTwo.MovementXValues, i), speedXValues[i]));
                movementYValues.Add(Euler(SetStartValues(movementYValues, stageTwo.MovementYValues, i), speedYValues[i]));
            }

            PrintParameters();
        }

        private double XFunction(double arg)
        {
            double baseAngle = stageTwo.stageOne.GetEndAngle() + stageTwo.GetEndAngle();
            double totalAngle = baseAngle + RotationAngleFunction() * arg;
            double mass = GetCurrentMass(arg);
            return (F * Math.Sin(totalAngle)) / mass;
        }

        private double YFunction(double arg)
        {
            double baseAngle = stageTwo.stageOne.GetEndAngle() + stageTwo.GetEndAngle();
            double totalAngle = baseAngle + RotationAngleFunction() * arg;
            double mass = GetCurrentMass(arg);
            return (F * Math.Cos(totalAngle)) / mass - g;
        }
    }
}