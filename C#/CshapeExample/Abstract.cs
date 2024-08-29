// 抽象基類 BankAccount
public abstract class BankAccount
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }

    // 抽象方法，沒有實現
    public abstract void Withdraw(decimal amount);

    // 普通方法，有實現
    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"Deposited {amount}. New balance: {Balance}");
        }
        else
        {
            Console.WriteLine("Deposit amount must be positive.");
        }
    }
}


// 派生類別 SavingsAccount
public class SavingsAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    // 覆寫抽象方法
    public override void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew {amount} from SavingsAccount. New balance: {Balance}");
        }
        else
        {
            Console.WriteLine("Insufficient balance or invalid amount for withdrawal.");
        }
    }
}

// 派生類別 CheckingAccount
public class CheckingAccount : BankAccount
{
    public decimal OverdraftLimit { get; set; }

    // 覆寫抽象方法
    public override void Withdraw(decimal amount)
    {
        if (amount > 0 && (Balance + OverdraftLimit) >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew {amount} from CheckingAccount. New balance: {Balance}");
        }
        else
        {
            Console.WriteLine("Overdraft limit exceeded or invalid amount for withdrawal.");
        }
    }
}

class AbstractClass
{
    public AbstractClass()
    {
        // 創建 SavingsAccount 和 CheckingAccount 的實例
        SavingsAccount savings = new SavingsAccount { AccountNumber = "S12345", Balance = 1000m, InterestRate = 0.05m };
        CheckingAccount checking = new CheckingAccount { AccountNumber = "C67890", Balance = 500m, OverdraftLimit = 200m };

        // 存款和取款操作
        savings.Deposit(200m);     // 輸出: Deposited 200. New balance: 1200
        savings.Withdraw(300m);    // 輸出: Withdrew 300 from SavingsAccount. New balance: 900

        checking.Deposit(100m);    // 輸出: Deposited 100. New balance: 600
        checking.Withdraw(700m);   // 輸出: Withdrew 700 from CheckingAccount. New balance: -100
    }
}

