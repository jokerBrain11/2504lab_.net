using System;

// 定義委派類型，用於事件
public delegate void EventHandler(string message);

// 事件發布者類別
class Publisher
{
    // 定義事件
    public event EventHandler RaiseEvent;

    // 引發事件的方法
    public void DoSomething()
    {
        Console.WriteLine("Doing something...");
        System.Threading.Thread.Sleep(3000);

        // 當事件有訂閱者時，引發事件
        RaiseEvent?.Invoke("Event triggered!");
    }
}

// 事件訂閱者類別
class Subscriber
{
    // 事件處理方法，與委派簽名匹配
    public void OnEventRaised(string message)
    {
        Console.WriteLine("Subscriber received message: " + message);
    }
}

class Event
{
    public Event()
    {
        // 創建事件發布者和訂閱者實例
        Publisher publisher = new Publisher();
        Subscriber subscriber = new Subscriber();

        // 訂閱事件
        publisher.RaiseEvent += subscriber.OnEventRaised;

        // 執行操作，引發事件
        publisher.DoSomething();
    }
}
