namespace webpai
{
    // 自訂Attribute為CustomAttribute
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class CustomAttribute : Attribute
    {
        public string Description { get; } 
        public CustomAttribute(string description)
        {
            Description = description;
        }
    }

    // 於class及method上使用CustomAttribute
    [Custom("This is a class attribute")]
    class AnotherClass
    {
        [Custom("This is a method attribute")]
        public void Method()
        {
            Console.WriteLine("Hello World!");
        }
    }

    class CustomAttributeExample
    {
        public CustomAttributeExample()
        {
            // 獲取 AnotherClass 類上的所有特性
            var classAttributes = typeof(AnotherClass).GetCustomAttributes(false);
            
            // 遍歷類上的所有特性
            foreach (var attribute in classAttributes)
            {
                // 檢查特性是否為 CustomAttribute 類型
                if (attribute is CustomAttribute customAttribute)
                {
                    // 輸出 CustomAttribute 的 Description 屬性值
                    Console.WriteLine(customAttribute.Description);
                }
            }

            // 獲取 AnotherClass 類中名為 Method 的方法上的所有特性
            var methodAttributes = typeof(AnotherClass).GetMethod("Method").GetCustomAttributes(false);
            
            // 遍歷方法上的所有特性
            foreach (var attribute in methodAttributes)
            {
                // 檢查特性是否為 CustomAttribute 類型
                if (attribute is CustomAttribute customAttribute)
                {
                    // 輸出 CustomAttribute 的 Description 屬性值
                    Console.WriteLine(customAttribute.Description);
                }
            }
        }
    }

    
}
