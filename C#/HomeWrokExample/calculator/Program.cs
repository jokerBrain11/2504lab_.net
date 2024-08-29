using System;

namespace SimpleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("請輸入第一個數字:");
                string? readLine1 = Console.ReadLine();
                if (string.IsNullOrEmpty(readLine1))
                {
                    Console.WriteLine("輸入內容不可為空，請重新輸入!");
                    continue;
                }
                if (!double.TryParse(readLine1, out double num1))
                {
                    Console.WriteLine("無效的數字，請重新輸入!");
                    continue;
                }

                Console.WriteLine("請輸入第二個數字:");
                string? readLine2 = Console.ReadLine();
                if (string.IsNullOrEmpty(readLine2))
                {
                    Console.WriteLine("輸入內容不可為空，請重新輸入!");
                    continue;
                }
                if (!double.TryParse(readLine2, out double num2))
                {
                    Console.WriteLine("無效的數字，請重新輸入!");
                    continue;
                }

                Console.WriteLine("請選擇運算類型 (+, -, *, /):");
                string? operation = Console.ReadLine();
                if (string.IsNullOrEmpty(operation))
                {
                    Console.WriteLine("運算符號內容不可為空，請重新輸入!");
                    continue;
                }

                double result = 0;

                switch (operation)
                {
                    case "+":
                        result = num1 + num2;
                        break;
                    case "-":
                        result = num1 - num2;
                        break;
                    case "*":
                        result = num1 * num2;
                        break;
                    case "/":
                        if (num2 != 0)
                        {
                            result = num1 / num2;
                        }
                        else
                        {
                            Console.WriteLine("除數不能為零。");
                            continue;
                        }
                        break;
                    default:
                        Console.WriteLine("無效的運算符。");
                        continue;
                }

                Console.WriteLine($"結果是: {result}");
                Console.WriteLine("是否繼續計算，請填入(Y/N)");
                string? continueCal = Console.ReadLine();
                if (string.IsNullOrEmpty(continueCal))
                {
                    Console.WriteLine("因為輸入為空，已預設N為結果結束計算機功能");
                    break;
                }

                string[] validInputs = new string[] { "Y", "N" };
                if (!validInputs.Contains(continueCal, StringComparer.OrdinalIgnoreCase))
                {
                    Console.WriteLine("輸入內容不正確，請重新輸入!");
                    continue;
                }

                if (continueCal.ToUpper() == "N") break;
            }
        }
    }
}
