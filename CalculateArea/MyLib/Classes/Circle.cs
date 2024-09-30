using System;
using MyLib.Interfaces;

namespace MyLib.Classes;

public class Circle : Figure
{
    private readonly double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }
    
    public override double CalculateArea() => Math.PI *  Math.Pow(_radius, 2);
    
    public override void PrintFigure()
    {
        Console.WriteLine($"Круг с радиусом: {_radius} и площадью: {CalculateArea()}");
    }
}