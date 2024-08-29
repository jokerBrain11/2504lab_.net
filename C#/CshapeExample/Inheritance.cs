// 基類 Vehicle
public class Vehicle
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public void Start()
    {
        Console.WriteLine("The vehicle is starting.");
    }

    public void Stop()
    {
        Console.WriteLine("The vehicle is stopping.");
    }
}

// 派生類別 Car
public class Car : Vehicle
{
    public int NumberOfDoors { get; set; }

    public void Honk()
    {
        Console.WriteLine("The car is honking: Beep! Beep!");
    }
}

// 派生類別 Bicycle
public class Bicycle : Vehicle
{
    public bool HasBell { get; set; }

    public void RingBell()
    {
        if (HasBell)
        {
            Console.WriteLine("The bicycle bell rings: Ring! Ring!");
        }
        else
        {
            Console.WriteLine("This bicycle has no bell.");
        }
    }
}

class InheritanceClass
{
    public InheritanceClass()
    {
        // 創建 Car 的實例
        Car car = new Car
        {
            Make = "Toyota",
            Model = "Camry",
            Year = 2020,
            NumberOfDoors = 4
        };

        car.Start();       // 輸出: The vehicle is starting.
        car.Honk();        // 輸出: The car is honking: Beep! Beep!
        car.Stop();        // 輸出: The vehicle is stopping.

        // 創建 Bicycle 的實例
        Bicycle bicycle = new Bicycle
        {
            Make = "Giant",
            Model = "Escape 3",
            Year = 2022,
            HasBell = true
        };

        bicycle.Start();    // 輸出: The vehicle is starting.
        bicycle.RingBell(); // 輸出: The bicycle bell rings: Ring! Ring!
        bicycle.Stop();     // 輸出: The vehicle is stopping.
    }
}
