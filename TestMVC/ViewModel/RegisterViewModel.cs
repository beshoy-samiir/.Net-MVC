using System.ComponentModel.DataAnnotations;
using TestMVC.Models;

namespace TestMVC.ViewModel
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
