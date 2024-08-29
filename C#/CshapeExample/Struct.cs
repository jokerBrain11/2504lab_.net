using System;

struct Point
{
    public int x;
    public int y;

    // 無參數建構子
    public Point()
    {
        x = -1;
        y = -1;
    }

    // 有參數建構子
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    // 打印點的坐標
    public void Print()
    {
        Console.WriteLine($"x: {x}, y: {y}");
    }

    // 重置點的坐標
    public void Reset()
    {
        // 使用默認構造函數的值來重置結構的狀態
        x = -1;
        y = -1;
    }
}

class StructClass
{
    public StructClass()
    {
        // 使用默認構造函數創建結構
        Point p1 = new Point(10, 20);
        p1.Print();
        p1.Reset();
        p1.Print();

        // 使用無參數建構子創建結構
        Point p2 = new Point();
        p2.Print();
        p2.Reset();
        p2.Print();
    }
}
