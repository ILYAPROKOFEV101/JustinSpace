namespace Calc;

public abstract class Stages
{
    protected double t; // Время работы ступени, с
    protected double F; // Тяга двигателя, Н
    protected double Fmin; // Минимальная тяга, Н
    protected double dryMass; // Масса ракеты без топлива
    protected double fuelMass; // Масса топлива
    protected double fuelConsumptionRate; // Расход топлива, кг/с
    protected double angle; // Угол поворота, градусы
    protected double g = 9.8; // Ускорение свободного падения
    protected double step = 0.01;

    protected List<double> timeValues = new List<double>();
    protected List<double> movementXValues = new List<double>();
    protected List<double> movementYValues = new List<double>();
    protected List<double> speedXValues = new List<double>();
    protected List<double> speedYValues = new List<double>();
    protected List<double> accelerationXValues = new List<double>();
    protected List<double> accelerationYValues = new List<double>();

    public List<double> TimeValues => timeValues;
    public List<double> MovementXValues => movementXValues;
    public List<double> MovementYValues => movementYValues;
    public List<double> SpeedXValues => speedXValues;
    public List<double> SpeedYValues => speedYValues;

    public void CalculateTimeValues()
    {
        for (double i = 0.0; i < t; i += step)
        {
            timeValues.Add(i);
        }
    }

    // Уточнённый метод Эйлера
    public double Euler(double arg1, double arg2)
    {
        return arg1 + 2 * step * arg2;
    }

    protected double SetStartValues(List<double> values, List<double> previousValues, int i)
    {
        if (i >= 2)
            return values[i - 2];
        else if (previousValues != null)
            return previousValues[previousValues.Count - 1 - (2 - i)];
        else
            return 0.0;
    }

    protected double RotationAngleFunction()
    {
        return angle * ((Math.PI / 2) / 90) / t;
    }

    public double GetEndAngle()
    {
        return RotationAngleFunction() * t;
    }

    protected void PrintParameters()
    {
        Console.WriteLine("Параметры в конце работы ступени " + this.GetType().Name);
        Console.WriteLine("X = " + Math.Round(movementXValues[^1]) + " м");
        Console.WriteLine("Y = " + Math.Round(movementYValues[^1]) + " м");

        var vx = speedXValues[^1];
        var vy = speedYValues[^1];
        var vTotal = Math.Sqrt(vx * vx + vy * vy);

        Console.WriteLine("Скорость по X: " + Math.Round(vx) + " м/с (" + Math.Round(vx * 3.6) + " км/ч)");
        Console.WriteLine("Скорость по Y: " + Math.Round(vy) + " м/с (" + Math.Round(vy * 3.6) + " км/ч)");
        Console.WriteLine("Суммарная скорость: " + Math.Round(vTotal) + " м/с (" + Math.Round(vTotal * 3.6) + " км/ч)");

        var ax = accelerationXValues[^1];
        var ay = accelerationYValues[^1];
        var aTotal = Math.Sqrt(ax * ax + ay * ay);

        Console.WriteLine("Ускорение по X: " + Math.Round(ax, 2) + " м/с²");
        Console.WriteLine("Ускорение по Y: " + Math.Round(ay, 2) + " м/с²");
        Console.WriteLine("Суммарное ускорение: " + Math.Round(aTotal, 2) + " м/с²");
        Console.WriteLine();
    }

    public virtual double GetCurrentMass(double time)
    {
        return dryMass + fuelMass - fuelConsumptionRate * time;
    }
}