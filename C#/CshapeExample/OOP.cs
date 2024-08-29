using System;

// 定義 Animal 類別
class Animal
{
    // 私有字段（屬性後面的底層儲存）
    private string _Name;
    private int _Age;
    private string _Color;

    // 公有屬性，用於訪問和設定字段
    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    public int Age
    {
        get { return _Age; }
        set { _Age = value; }
    }

    public string Color
    {
        // 可在 set 中加入條件判斷
        set { _Color = (value == "White") ? "No Color" : value; }
        get { return _Color; }
    }

    // 無參數建構子，初始化字段的預設值
    public Animal()
    {
        _Name = "No Name";
        _Age = 0;
        _Color = "No Color";
    }

    // 有參數建構子，允許使用者設定初始值
    public Animal(string name, int age, string color)
    {
        _Name = name;
        _Age = age;
        _Color = color;
    }

    // 顯示動物資訊的方法
    public void Info()
    {
        Console.WriteLine($"Name: {_Name}, Age: {_Age}, Color: {_Color}");
        Console.WriteLine("===================================");
    }

    // 虛擬方法，可在派生類別中覆寫
    public virtual void Bark()
    {
        Console.WriteLine("default speak");
    }
}

// 定義靜態類別 Math
static class Math
{
    // 靜態成員，所有實例共享
    public static int A { get; set; }
    public static int B { get; set; }
    public static string Name;

    // 靜態方法，用於靜態類別的運算
    public static void Add()
    {
        Console.WriteLine("Static Add: " + (A + B));
        Console.WriteLine("===================================");
    }

    public static void Sub()
    {
        Console.WriteLine("Static Sub: " + (A - B));
        Console.WriteLine("===================================");
    }
}

// 定義普通類別 Math1
public class Math1
{
    // 靜態屬性
    public static int A { get; set; }
    public static int B { get; set; }

    // 非靜態字段
    public int a;
    public int b;

    // 靜態方法，只能存取靜態成員
    public static void AddWithStatic()
    {
        int staticAdd = A + B;
        Console.WriteLine("Static Add: " + staticAdd);
        Console.WriteLine("===================================");
    }

    // 非靜態方法，可以存取靜態和非靜態成員
    public void AddWithNonStaticAndStatic()
    {
        int add = a + b;        // 非靜態成員相加
        int staticAdd = A + B;  // 靜態成員相加
        int mixAdd = a + B;     // 非靜態和靜態成員相加
        Console.WriteLine("Add: " + add);
        Console.WriteLine("Static Add: " + staticAdd);
        Console.WriteLine("Mix Add: " + mixAdd);
        Console.WriteLine("===================================");
    }
}

// 主程式類別
class OOP
{
    public OOP()
    {
        // 使用 new 建立 Animal 物件，並設定屬性
        Animal animal = new Animal();
        animal.Name = "animal";
        animal.Age = 3;
        animal.Color = "Black";
        animal.Info();

        // 建立帶參數的 Animal 物件
        Animal cat = new Animal("Cat", 2, "White");
        cat.Info();

        // 使用靜態類別 Math
        Console.WriteLine("靜態成員使用");
        Math.A = 10;
        Math.B = 5;
        Math.Add();
        Math.Sub();

        // 使用混合靜態和非靜態成員的類別 Math1
        Console.WriteLine("靜態與非靜態混用");
        Math1.A = 10;
        Math1.B = 5;
        Math1.AddWithStatic();

        Math1 math1 = new Math1();
        math1.a = 10;
        math1.b = 5;
        math1.AddWithNonStaticAndStatic();
    }
}
