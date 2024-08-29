using System;

// 定義一個委派類型
public delegate void Notify(string message);

class Delegate
{
    // 定義一個方法與委派簽名匹配
    public static void DisplayMessage(string message)
    {
        Console.WriteLine("Message: " + message);
    }

    public Delegate()
    {
        // 創建委派實例，指向 DisplayMessage 方法
        Notify notifier = new Notify(DisplayMessage);

        // 呼叫方法，使用委派進行回呼
        notifier("Hello, this is a delegate example!");
    }
}
