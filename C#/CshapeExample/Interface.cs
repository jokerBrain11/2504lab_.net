// 定義接口 IPayment
public interface IPayment
{
    void Pay(decimal amount);
}

// 實現 IPayment 接口的類別 CreditCardPayment
public class CreditCardPayment : IPayment
{
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string ExpiryDate { get; set; }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using Credit Card.");
    }
}

// 實現 IPayment 接口的類別 PayPalPayment
public class PayPalPayment : IPayment
{
    public string Email { get; set; }

    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using PayPal.");
    }
}

class Interface
{
    public Interface()
    {
        // 創建 CreditCardPayment 和 PayPalPayment 的實例
        IPayment creditCardPayment = new CreditCardPayment
        {
            CardNumber = "1234-5678-9876-5432",
            CardHolderName = "John Doe",
            ExpiryDate = "12/25"
        };

        IPayment payPalPayment = new PayPalPayment
        {
            Email = "john.doe@example.com"
        };

        // 使用接口來處理支付
        ProcessPayment(creditCardPayment, 100.00m);
        ProcessPayment(payPalPayment, 150.00m);
    }

    static void ProcessPayment(IPayment paymentMethod, decimal amount)
    {
        paymentMethod.Pay(amount);
    }
}
