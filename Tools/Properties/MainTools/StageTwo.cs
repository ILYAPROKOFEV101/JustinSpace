using System;

namespace Tools.Properties.MainTools
{
    public class StageTwo : Stages
    {
        public readonly StageOne stageOne;

        public StageTwo(StageOne stageOne, double dryMass, double fuelMass, double fuelConsumptionRate)
        {
            this.stageOne = stageOne;

            this.dryMass = dryMass;
            this.fuelMass = fuelMass;
            this.fuelConsumptionRate = fuelConsumptionRate;

            t = 182.0;
            F = 921200.0;
            angle = 16.0;

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

                speedXValues.Add(Euler(SetStartValues(speedXValues, stageOne.SpeedXValues, i), accelerationXValues[i]));
                speedYValues.Add(Euler(SetStartValues(speedYValues, stageOne.SpeedYValues, i), accelerationYValues[i]));

                movementXValues.Add(Euler(SetStartValues(movementXValues, stageOne.MovementXValues, i), speedXValues[i]));
                movementYValues.Add(Euler(SetStartValues(movementYValues, stageOne.MovementYValues, i), speedYValues[i]));
            }

            PrintParameters();
        }

        private double XFunction(double arg)
        {
            double totalAngle = stageOne.GetEndAngle() + RotationAngleFunction() * arg;
            double mass = GetCurrentMass(arg);
            return (F * Math.Sin(totalAngle)) / mass;
        }

        private double YFunction(double arg)
        {
            double totalAngle = stageOne.GetEndAngle() + RotationAngleFunction() * arg;
            double mass = GetCurrentMass(arg);
            return (F * Math.Cos(totalAngle)) / mass - g;
        }
    }
}