using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools.Properties.MainTools
{
      public class Calculator
    {
        private List<double> xAxisValues = new List<double>();
        private List<double> yAxisValues = new List<double>();
        private double r = 6371000.0; // Радиус Земли

        public StageOne StageOne { get; }
        public StageTwo StageTwo { get; }
        public StageThree StageThree { get; }

        public Calculator(double dryMass1, double fuelMass1, double fuelConsumption1,
                          double dryMass2, double fuelMass2, double fuelConsumption2,
                          double dryMass3, double fuelMass3, double fuelConsumption3)
        {
            StageOne stageOne = new StageOne(dryMass1, fuelMass1, fuelConsumption1);
            StageTwo stageTwo = new StageTwo(stageOne, dryMass2, fuelMass2, fuelConsumption2);
            StageThree stageThree = new StageThree(stageTwo, dryMass3, fuelMass3, fuelConsumption3);

            this.StageOne = stageOne;
            this.StageTwo = stageTwo;
            this.StageThree = stageThree;

            EvaluateParameters();
            CalculateTrajectory();
        }

        private void EvaluateParameters()
        {
            StageOne.CalculateFunction();
            StageTwo.CalculateFunction();
            StageThree.CalculateFunction();
        }

        private void CalculateTrajectory()
        {
            xAxisValues.AddRange(StageOne.MovementXValues);
            xAxisValues.AddRange(StageTwo.MovementXValues);
            xAxisValues.AddRange(StageThree.MovementXValues);

            yAxisValues.AddRange(StageOne.MovementYValues);
            yAxisValues.AddRange(StageTwo.MovementYValues);
            yAxisValues.AddRange(StageThree.MovementYValues);

            CurvatureCorrection();

            int stageOneCount = StageOne.TimeValues.Count;
            int stageTwoCount = StageTwo.TimeValues.Count;

            Console.WriteLine("Высота полёта после отстыковки второй ступени: " +
                              $"{Math.Round(yAxisValues[stageOneCount + stageTwoCount] / 1000)} км");

            Console.WriteLine("Конечная высота орбиты: " +
                              $"{Math.Round(yAxisValues[yAxisValues.Count - 1] / 1000)} км");

            Console.WriteLine("Vmax1 = " + StageOne.SpeedYValues.Max());
            Console.WriteLine("Vmax2 = " + StageTwo.SpeedYValues.Max());
        }

        private void CurvatureCorrection()
        {
            for (int i = 0; i < xAxisValues.Count; i++)
            {
                double h = r / Math.Cos(Math.Atan(xAxisValues[i] / r)) - r;
                yAxisValues[i] += h;
            }
        }
        

        public List<double> XAxisValues => xAxisValues;
        public List<double> YAxisValues => yAxisValues;
        public List<double> SpeedValues
        {
            get
            {
                var allSpeeds = new List<double>();
                allSpeeds.AddRange(StageOne.SpeedYValues);
                allSpeeds.AddRange(StageTwo.SpeedYValues);
                allSpeeds.AddRange(StageThree.SpeedYValues);
                return allSpeeds;
            }
        }

        public List<int> StageIndices
        {
            get
            {
                var indices = new List<int>();
                indices.AddRange(Enumerable.Repeat(1, StageOne.SpeedYValues.Count));
                indices.AddRange(Enumerable.Repeat(2, StageTwo.SpeedYValues.Count));
                indices.AddRange(Enumerable.Repeat(3, StageThree.SpeedYValues.Count));
                return indices;
            }
        }
        public List<double> FuelLeftValues
        {
            get
            {
                var allFuelLeft = new List<double>();
                allFuelLeft.AddRange(StageOne.FuelLeftValues);
                allFuelLeft.AddRange(StageTwo.FuelLeftValues);
                allFuelLeft.AddRange(StageThree.FuelLeftValues);
                return allFuelLeft;
            }
        }
    }
}