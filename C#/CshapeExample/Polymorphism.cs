// 基類 Shape
public abstract class Shape
{
    public string Name { get; set; }

    // 抽象方法 CalculateArea，要求派生類別必須實現
    public abstract double CalculateArea();
}

// 派生類別 Circle
public class Circle : Shape
{
    public double Radius { get; set; }

    // 覆寫 CalculateArea 方法
    public override double CalculateArea()
    {
        return 3.14 * Radius * Radius;
    }
}

// 派生類別 Rectangle
public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    // 覆寫 CalculateArea 方法
    public override double CalculateArea()
    {
        return Width * Height;
    }
}

// 派生類別 Triangle
public class Triangle : Shape
{
    public double Base { get; set; }
    public double Height { get; set; }

    // 覆寫 CalculateArea 方法
    public override double CalculateArea()
    {
        return 0.5 * Base * Height;
    }
}

class Polymorphism
{
    public Polymorphism()
    {
        // 創建各種形狀的實例
        Shape circle = new Circle { Name = "Circle", Radius = 5 };
        Shape rectangle = new Rectangle { Name = "Rectangle", Width = 4, Height = 6 };
        Shape triangle = new Triangle { Name = "Triangle", Base = 4, Height = 3 };

        // 建立一個形狀的列表
        List<Shape> shapes = new List<Shape> { circle, rectangle, triangle };

        // 使用多型來計算每個形狀的面積
        foreach (var shape in shapes)
        {
            Console.WriteLine($"The area of the {shape.Name} is {shape.CalculateArea()}");
        }
    }
}
