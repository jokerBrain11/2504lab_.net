class Programs
{
    static void Main(string[] args)
    {
        // #region Value Types
        // int a = 10;
        // Console.WriteLine("Value of a: " + a);
        // int b = a;
        // Console.WriteLine("Value of b: " + b);
        // b = 20;
        // Console.WriteLine(string.Format(
        //     "Value of a: {0}, Value of b: {1}", a, b));

        // #endregion

        // #region 整數型別
        // byte myByte = 255;
        // sbyte mySbyte = -128;
        // short myShort = 32767;
        // ushort myUshort = 65535;
        // int myInt = 2147483647;
        // uint myUint = 4294967295;
        // long myLong = 9223372036854775807;
        // ulong myUlong = 18446744073709551615;

        // Console.WriteLine($"byte: {myByte}");
        // Console.WriteLine($"sbyte: {mySbyte}");
        // Console.WriteLine($"short: {myShort}");
        // Console.WriteLine($"ushort: {myUshort}");
        // Console.WriteLine($"int: {myInt}");
        // Console.WriteLine($"uint: {myUint}");
        // Console.WriteLine($"long: {myLong}");
        // Console.WriteLine($"ulong: {myUlong}");
        // #endregion
        
        // #region 浮點數型別
        // float myFloat = 3.141592653589793238f;
        // double myDouble = 3.141592653589793238;
        // decimal myDecimal = 3.141592653589793238m;

        // Console.WriteLine($"float: {myFloat}");
        // Console.WriteLine($"double: {myDouble}");
        // Console.WriteLine($"decimal: {myDecimal}");
        // #endregion

        // #region 字元型別
        // char myChar = 'A';
        // Console.WriteLine($"char: {myChar}");
        // #endregion
    
        // #region 布林型別
        // bool myBool = true;
        // Console.WriteLine($"bool: {myBool}");
        // #endregion
    
        // #region struct 練習
        // // 啟用Struct.cs的程式碼
        // StructClass structClass = new StructClass();
        // #endregion
    
        // #region enum 練習
        // // 啟用Enum.cs的程式碼
        // EnumClass enumClass = new EnumClass();
        // #endregion

        // #region string 練習
        // // 字串宣告
        // string myString = "Hello World!";
        // Console.WriteLine($"字串宣告 : {myString}");
        // Console.WriteLine("====================================");
        // // 字串長度
        // Console.WriteLine($"Length: {myString.Length}");
        // Console.WriteLine("====================================");
        // // 字串連接
        // string myString1 = "Hello";
        // string myString2 = "World!";
        // string myString3 = myString1 + " " + myString2;
        // Console.WriteLine("字串連接 : " + myString3);
        // Console.WriteLine("====================================");
        // // 字串插值
        // string myString4 = $"Hello {myString2}";
        // Console.WriteLine("字串插值 : " + myString4);
        // Console.WriteLine("====================================");
        // // 字串格式化
        // string myString5 = string.Format("Hello {0}", myString2);
        // Console.WriteLine("字串格式化 : " + myString5);
        // Console.WriteLine("====================================");
        // // 字串比較
        // string myString6 = "Hello";
        // string myString7 = "World!";
        // Console.WriteLine("字串比較 : " + myString6.Equals(myString7));
        // Console.WriteLine("字串比較 : " + myString6.CompareTo(myString7));
        // Console.WriteLine("====================================");
        // // 字串搜尋
        // string myString8 = "Hello World!";
        // Console.WriteLine("字串搜尋 : " + myString8.Contains("World"));
        // Console.WriteLine("====================================");
        // // 字串取代
        // string myString9 = "Hello World!";
        // Console.WriteLine("字串取代 : " + myString9.Replace("World", "C#"));
        // Console.WriteLine("====================================");
        // // 字串分割
        // string myString10 = "Hello,World!";
        // string[] myStringArray = myString10.Split(',');
        // Console.WriteLine("字串分割 : ");
        // foreach (var item in myStringArray)
        // {
        //     Console.WriteLine(item);
        // }
        // Console.WriteLine("====================================");
        // // 字串轉換
        // string myString11 = "10";
        // int myInt = int.Parse(myString11);
        // Console.WriteLine(myInt);
        // Console.WriteLine("====================================");
        // // 字串轉換
        // int myInt1 = 10;
        // string myString12 = myInt1.ToString();
        // Console.WriteLine(myString12);
        // Console.WriteLine("====================================");
        // // 字串轉換
        // int myInt2 = 10;
        // string myString13 = Convert.ToString(myInt2);
        // Console.WriteLine(myString13);
        // Console.WriteLine("====================================");
        // // 字串轉換
        // string myString14 = "10";
        // int myInt3;
        // bool result = int.TryParse(myString14, out myInt3);
        // if (result)
        // {
        //     Console.WriteLine(myInt3);
        // }
        // else
        // {
        //     Console.WriteLine("轉換失敗");
        // }
        // Console.WriteLine("====================================");
        // // 字串轉換
        // string myString15 = "10.5";
        // float myFloat;
        // bool result1 = float.TryParse(myString15, out myFloat);
        // if (result1)
        // {
        //     Console.WriteLine(myFloat);
        // }
        // else
        // {
        //     Console.WriteLine("轉換失敗");
        // }
        // Console.WriteLine("====================================");
        // #endregion
    
        // #region array 練習
        // // 宣告陣列
        // int[] myArray = new int[5];
        // myArray[0] = 10;
        // myArray[1] = 20;
        // myArray[2] = 30;
        // myArray[3] = 40;
        // myArray[4] = 50;
        // Console.WriteLine("宣告陣列 : ");
        // foreach (var item in myArray)
        // {
        //     Console.WriteLine(item);
        // }
        // Console.WriteLine("====================================");
        // // 宣告陣列
        // int[] myArray1 = new int[] { 10, 20, 30, 40, 50 };
        // Console.WriteLine("宣告陣列 : ");
        // foreach (var item in myArray1)
        // {
        //     Console.WriteLine(item);
        // }
        // Console.WriteLine("====================================");
        // // 宣告陣列
        // int[] myArray2 = { 10, 20, 30, 40, 50 };
        // Console.WriteLine("宣告陣列 : ");
        // foreach (var item in myArray2)
        // {
        //     Console.WriteLine(item);
        // }
        // Console.WriteLine("====================================");
        // // 宣告多維陣列
        // int[,] myArray3 = new int[2, 3];
        // myArray3[0, 0] = 10;
        // myArray3[0, 1] = 20;
        // myArray3[0, 2] = 30;
        // myArray3[1, 0] = 40;
        // myArray3[1, 1] = 50;
        // myArray3[1, 2] = 60;
        // Console.WriteLine("宣告多維陣列 : ");
        // for (int i = 0; i < 2; i++)
        // {
        //     for (int j = 0; j < 3; j++)
        //     {
        //         Console.WriteLine(myArray3[i, j]);
        //     }
        // }
        // Console.WriteLine("====================================");
        // // 宣告多維陣列
        // int[,] myArray4 = new int[,] { { 10, 20, 30 }, { 40, 50, 60 } };
        // Console.WriteLine("宣告多維陣列 : ");
        // for (int i = 0; i < 2; i++)
        // {
        //     for (int j = 0; j < 3; j++)
        //     {
        //         Console.WriteLine(myArray4[i, j]);
        //     }
        // }
        // Console.WriteLine("====================================");
        // // 陣列方法
        // int[] myArray5 = { 10, 20, 30, 40, 50 };
        // Console.WriteLine("陣列方法 : ");
        // Console.WriteLine("Length: " + myArray5.Length);
        // Console.WriteLine("IndexOf: " + Array.IndexOf(myArray5, 30));
        // Console.WriteLine("Clear: ");
        // // 清除陣列元素(從索引0開始，清除2個元素)
        // Array.Clear(myArray5, 0, 2);
        // foreach (var item in myArray5)
        // {
        //     Console.WriteLine(item);
        // }
        // Console.WriteLine("Copy: ");
        // int[] myArray6 = new int[3];
        // Array.Copy(myArray5, myArray6, 3);
        // foreach (var item in myArray6)
        // {
        //     Console.WriteLine(item);
        // }
        // Console.WriteLine("Sort: ");
        // Array.Sort(myArray5);
        // foreach (var item in myArray5)
        // {
        //     Console.WriteLine(item);
        // }
        // Console.WriteLine("Reverse: ");
        // Array.Reverse(myArray5);
        // foreach (var item in myArray5)
        // {
        //     Console.WriteLine(item);
        // }
        // Console.WriteLine("====================================");
        // // 宣告不規則陣列
        // int[][] myArray7 = new int[2][];
        // myArray7[0] = new int[] { 10, 20, 30 };
        // myArray7[1] = new int[] { 40, 50, 60 };
        // Console.WriteLine("宣告不規則陣列 : ");
        // for (int i = 0; i < 2; i++)
        // {
        //     for (int j = 0; j < 3; j++)
        //     {
        //         Console.WriteLine(myArray7[i][j]);
        //     }
        // }
        // Console.WriteLine("====================================");
        // // 宣告不規則陣列
        // int[][] myArray8 = new int[][]
        // {
        //     new int[] { 10, 20, 30 },
        //     new int[] { 40, 50, 60 }
        // };
        // Console.WriteLine("宣告不規則陣列 : ");
        // for (int i = 0; i < 2; i++)
        // {
        //     for (int j = 0; j < 3; j++)
        //     {
        //         Console.WriteLine(myArray8[i][j]);
        //     }
        // }
        // Console.WriteLine("====================================");
        // #endregion
    
        // #region oop 練習
        // // 啟用OOP.cs的程式碼
        // OOP oOP = new OOP();
        // #endregion

        // #region interface 練習
        // // 啟用Interface.cs的程式碼
        // Interface interfaceClass = new Interface();
        // #endregion

        // #region polymorphism 練習
        // // 啟用Polymorphism.cs的程式碼
        // Polymorphism polymorphism = new Polymorphism();
        // #endregion

        // #region inheritance 練習
        // // 啟用Inheritance.cs的程式碼
        // InheritanceClass inheritance
        //     = new InheritanceClass();
        // #endregion

        // #region abstract 練習
        // // 啟用abstract.cs的程式碼
        // AbstractClass abstractClass = new AbstractClass();
        // #endregion

        // #region Delegate 練習
        // // 啟用Delegate.cs的程式碼
        // Delegate delegateClass = new Delegate();
        // #endregion

        // #region Event 練習
        // // 啟用Event.cs的程式碼
        // Event eventClass = new Event ();
        // #endregion

        // #region Lambda 練習
        // // 啟用Lambda.cs的程式碼
        // LambdaClass lambdaClass = new LambdaClass();
        // #endregion

        // #region LINQ 練習
        // // 啟用LINQ.cs的程式碼
        // LINQClass lINQClass = new LINQClass();
        // #endregion
    }
}