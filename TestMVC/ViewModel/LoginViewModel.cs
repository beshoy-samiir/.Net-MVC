using System.ComponentModel.DataAnnotations;

namespace TestMVC.ViewModel
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool RememberMe { get; set; }
    }
}
