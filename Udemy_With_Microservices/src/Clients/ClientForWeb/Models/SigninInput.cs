using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClientForWeb.Models
{
    public class SigninInput
    {
        [DisplayName("E-Mail")]
        public string Email { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
