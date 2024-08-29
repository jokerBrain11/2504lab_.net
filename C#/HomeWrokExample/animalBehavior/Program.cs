using System;

namespace AnimalBehavior
{
    // 定義一個抽象的 Animal 類別
    public abstract class Animal
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Animal(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // 抽象方法，子類別必須實現
        public abstract void MakeSound();
        public abstract void Move();
    }

    // 貓類別，繼承自 Animal
    public class Cat : Animal
    {
        public Cat(string name, int age) : base(name, age) { }

        // 實作貓的叫聲
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Meow!");
        }

        // 實作貓的移動方式
        public override void Move()
        {
            Console.WriteLine($"{Name} is gracefully walking.");
        }
    }

    // 狗類別，繼承自 Animal
    public class Dog : Animal
    {
        public Dog(string name, int age) : base(name, age) { }

        // 實作狗的叫聲
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Woof!");
        }

        // 實作狗的移動方式
        public override void Move()
        {
            Console.WriteLine($"{Name} is running energetically.");
        }
    }

    // 鳥類別，繼承自 Animal
    public class Bird : Animal
    {
        public Bird(string name, int age) : base(name, age) { }

        // 實作鳥的叫聲
        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: Tweet!");
        }

        // 實作鳥的移動方式
        public override void Move()
        {
            Console.WriteLine($"{Name} is flying high.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 創建不同的動物物件
            Animal cat = new Cat("Tom", 3);
            Animal dog = new Dog("Rex", 5);
            Animal bird = new Bird("Tweety", 2);

            // 使用多態性來處理不同動物的行為
            Animal[] animals = { cat, dog, bird };

            foreach (var animal in animals)
            {
                Console.WriteLine($"Animal: {animal.Name}, Age: {animal.Age}");
                animal.MakeSound();
                animal.Move();
                Console.WriteLine();
            }
        }
    }
}
