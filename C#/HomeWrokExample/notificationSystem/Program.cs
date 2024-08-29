using System;

namespace NotificationSystem
{
    // 事件發布者類別
    public class EventPublisher
    {
        // 定義委派，用於處理事件通知
        public delegate void NotifyEventHandler(object sender, EventArgs e);

        // 定義事件，使用前面定義的委派
        public event NotifyEventHandler NotifyEvent;

        // 觸發事件的方法
        public void TriggerEvent()
        {
            // 調用事件處理方法，通知所有訂閱者
            Console.WriteLine("觸發事件...");
            // 模擬事件處理過程
            Task.Delay(3000).Wait();
            // 通知事件
            OnNotifyEvent();
        }

        // 受保護的虛擬方法，用於觸發事件
        protected virtual void OnNotifyEvent()
        {
            // 檢查是否有訂閱者，若有則通知所有訂閱者
            if (NotifyEvent != null)
            {
                // 觸發事件，傳遞當前對象和空的事件參數
                NotifyEvent(this, EventArgs.Empty);
            }
        }
    }

    // 事件訂閱者類別
    public class EventSubscriber
    {
        // 當事件被觸發時呼叫的方法
        public void OnNotify(object sender, EventArgs e)
        {
            Console.WriteLine("接收到通知：事件已被觸發！");
        }
    }

    // 主程式類別
    class Program
    {
        static void Main(string[] args)
        {
            // 創建事件發布者的實例
            EventPublisher publisher = new EventPublisher();

            // 創建事件訂閱者的實例
            EventSubscriber subscriber = new EventSubscriber();

            // 訂閱事件，將訂閱者的方法與發布者的事件連接
            publisher.NotifyEvent += subscriber.OnNotify;

            // 觸發事件
            publisher.TriggerEvent();
        }
    }
}
