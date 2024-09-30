using MyLib.Interfaces;

namespace MyLib.Classes;

public abstract class Figure : IPrintFigure
{
    public abstract double CalculateArea();

    public abstract void PrintFigure();
}