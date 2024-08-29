enum Days
{
    // 使用預設值，會以0開始
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

enum Months
{
    // 使用指定值，後續列舉值會依序遞增
    January = 1,
    February,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December
}


class EnumClass
{
    public EnumClass() 
    {
        // 使用預設值會以0開始
        Days day1 = Days.Sunday;
        Days day5 = Days.Thursday;
        Console.WriteLine($"Sunday: {(int)day1}, Friday: {(int)day5}");
        // 使用指定值
        Months month1 = Months.January;
        Months month12 = Months.December;
        Console.WriteLine($"January: {(int)month1}, December: {(int)month12}");
        Console.WriteLine("====================================");
        // isDefined 判斷是否有該列舉值
        Console.WriteLine(Enum.IsDefined(typeof(Days), 7));
        Console.WriteLine(Enum.IsDefined(typeof(Days), "Sunday"));
        Console.WriteLine("====================================");
        // 使用GetValues 取得所有列舉'值'(回傳型態為 Array)
        foreach (var day in Enum.GetValues(typeof(Months)))
        {
            int dayValue = (int)day; // 將 enum 值轉換為對應的整數值
            Console.WriteLine($"Day: {day}, Value: {dayValue}");
        }
            Console.WriteLine("====================================");
        // 使用GetNames 取得所有列舉'名稱'(回傳型態為 string[])
        foreach (var month in Enum.GetNames(typeof(Months)))
        {
            // 無法直接取得列舉值，需轉換為對應的整數值
            // int dayValue = (int)day; 
            Console.WriteLine(month);
        }
    }
}