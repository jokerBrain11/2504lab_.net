class LambdaClass
{
    // 定義一個委派類型
    public delegate int MathOperation(int x, int y);
    public delegate void NotificationEventHandler(string message);
    public delegate void PrintMessage(string message);

    // 定義一個事件發布者類別
    class Publisher
    {
        // 定義事件
        public event NotificationEventHandler Notify;

        // 方法來引發事件
        public void RaiseEvent(string message)
        {
            Notify?.Invoke(message);
        }
    }

    public LambdaClass()
    {
        // 範例 1：使用 Lambda 表達式建立簡單的委派
        MathOperation add = (a, b) => a + b;
        MathOperation multiply = (a, b) => a * b;
        Console.WriteLine("Sum: " + add(5, 3)); // 8
        Console.WriteLine("Product: " + multiply(5, 3)); // 15

        Console.WriteLine("===========================");

        // 範例 2：使用 Lambda 表達式進行事件處理
        Publisher publisher = new Publisher();
        publisher.Notify += message => Console.WriteLine("Received message: " + message);
        publisher.RaiseEvent("Hello, this is an event notification!");

        Console.WriteLine("===========================");

        // 範例 3：使用 Lambda 表達式進行 LINQ 查詢
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
        Console.WriteLine("Even numbers:");
        foreach (var num in evenNumbers)
        {
            Console.WriteLine(num);
        }

        Console.WriteLine("===========================");

        // 範例 4：使用 Action 定義沒有返回值的方法
        Action<string> printMessage = message => Console.WriteLine("Action Message: " + message);
        printMessage("Hello from Action!");

        // 使用 Action 定義一個接受兩個參數的方法
        Action<int, int> addNumbersAction = (a, b) => Console.WriteLine("Sum: " + (a + b));
        addNumbersAction(5, 3);

        // 定義 Action 方法，無參數
        Action greet = () => Console.WriteLine("Hello, World!");
        greet();

        Console.WriteLine("===========================");

        // 範例 5：使用 Func 定義有返回值的方法
        Func<int, int, int> addNumbersFunc = (a, b) => a + b;
        int sum = addNumbersFunc(10, 20);
        Console.WriteLine("Sum: " + sum);

        // 使用 Func 定義一個返回 string 的方法
        Func<string, string> greetFunc = name => "Hello, " + name;
        string message = greetFunc("Alice");
        Console.WriteLine(message);

        // 使用 Func 定義一個返回 bool 的方法
        Func<int, bool> isEven = number => number % 2 == 0;
        bool result = isEven(4);
        Console.WriteLine("Is 4 even? " + result);

        Console.WriteLine("===========================");

        // 範例 6：Lambda 表達式與匿名方法的比較
        PrintMessage anonymousMethod = delegate (string msg)
        {
            Console.WriteLine("Anonymous Method: " + msg);
        };
        PrintMessage lambdaExpression = msg => Console.WriteLine("Lambda Expression: " + msg);
        anonymousMethod("Hello from anonymous method!");
        lambdaExpression("Hello from lambda expression!");
    }
}
