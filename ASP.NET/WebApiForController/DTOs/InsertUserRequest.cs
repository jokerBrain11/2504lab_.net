using System.ComponentModel.DataAnnotations;

namespace WebApiForController.DTOs
{
    public class InsertUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        [RegularExpression(@"^\d{4}/\d{2}/\d{2}$", ErrorMessage = "Birthday format is invalid")]
        public string Birthday { get; set; }
    }
}
