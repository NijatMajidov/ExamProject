using System.ComponentModel.DataAnnotations;

namespace RocketMVC.DTOs.Account
{
    public class LoginDto
    {
        [Required]
        public string EmailOrUser { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember {  get; set; }
    }
}
