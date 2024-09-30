using MyLib.Classes;

namespace TestProject1;

public class FigureTests
{
    [Test]
    public void TestCircleArea()
    {
        Circle circle = new Circle(4);
        
        double result = Math.PI * Math.Pow(4, 2);
        
        double actualArea = circle.CalculateArea();
        
        Assert.That(actualArea, Is.EqualTo(result).Within(0.001));
    }

    [Test]
    public void TestTriangleArea1()
    {
        Triangle triangle = new Triangle(3, 4, 5);
        double p = (3 + 4 + 5) / 2;
        double result = Math.Pow(p * (p - 3) * (p - 4) * (p - 5), 0.5);

        double actualArea = triangle.CalculateArea();
        
        Assert.That(actualArea, Is.EqualTo(result).Within(0.001));
    }
    
    [Test]
    public void TestTriangleArea2()
    {
        Triangle triangle = new Triangle(3, 4, 5);
        bool result = triangle.IsRectangular();
        
        Assert.That(true, Is.EqualTo(result).Within(0.001));
    }
}

