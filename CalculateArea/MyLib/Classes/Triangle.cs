using System;
using MyLib.Interfaces;

namespace MyLib.Classes;

public class Triangle : Figure
{
    private readonly double _side1;
    private readonly double _side2;
    private readonly double _side3;

    public Triangle(double side1, double side2, double side3)
    {
        _side1 = side1;
        _side2 = side2;
        _side3 = side3;
    }

    public double Semperimeter() => (_side1 + _side2 + _side3) / 2;

    public override double CalculateArea()
    {
        double semperimeter = Semperimeter();
        return Math.Pow((semperimeter * (semperimeter - _side1) * (semperimeter - _side2) * (semperimeter - _side3)),
            0.5);
    }

    public bool IsRectangular()
    {
        if (Math.Pow(_side1, 2) + Math.Pow(_side2, 2) - Math.Pow(_side3, 2) == 0) return true;
        if (Math.Pow(_side2, 2) + Math.Pow(_side3, 2) - Math.Pow(_side1, 2) == 0) return true;
        if (Math.Pow(_side1, 2) + Math.Pow(_side3, 2) - Math.Pow(_side2, 2) == 0) return true;
        
        return false;
    }

    public override void PrintFigure()
    {
        Console.WriteLine($"Треугольник со сторонами: ({_side1}, {_side2}, {_side3}) и площадью: {CalculateArea()}");
    }
}