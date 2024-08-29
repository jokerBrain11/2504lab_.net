using System;
using System.Linq;
using System.Reflection;

namespace requiredAttribute
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredAttribute : Attribute
    {
        public string ErrorMessage { get; }
        public RequiredAttribute(string errorMessage = "This field is required.")
        {
            ErrorMessage = errorMessage;
        }
    }

    public class Validator
    {
        public static bool ValidateRequired(object obj, out string errorMessage)
        {
            errorMessage = string.Empty;
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();

                if (requiredAttribute != null)
                {
                    var value = property.GetValue(obj);

                    if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                    {
                        errorMessage = requiredAttribute.ErrorMessage;
                        return false;
                    }
                }
            }

            return true;
        }
    }

    public class User
    {
        [RequiredAttribute("User Name is required.")]
        public string UserName { get; set; }

        [Required("Email is required.")]
        public string Email { get; set; }

        public int Age { get; set; }
    }

    public class RequiredAttributeExample
    {
        public RequiredAttributeExample()
        {
            var user = new User
            {
                UserName = null, // UserName 是必填項，這裡沒有設置值
                Email = "example@example.com"
            };

            if (!Validator.ValidateRequired(user, out string errorMessage))
            {
                Console.WriteLine($"Validation Failed: {errorMessage}");
            }
            else
            {
                Console.WriteLine("Validation Succeeded");
            }
        }
    }
}



