
public class SerializableAttributeExample
{
    public SerializableAttributeExample()
    {
        var type = typeof(User);
        // 檢查類別是否有 SerializableAttribute 屬性
        var isSerializable = type.GetCustomAttributes(typeof(SerializableAttribute), false).Any();
        if (isSerializable)
        {
            Console.WriteLine("The User class is serializable.");
        }
        else
        {
            Console.WriteLine("The User class is not serializable.");
        }
    }
}

// 指示可以使用二進位或 XML 序列化來序列化類別。該類別不能被繼承。
[Serializable]
public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
}