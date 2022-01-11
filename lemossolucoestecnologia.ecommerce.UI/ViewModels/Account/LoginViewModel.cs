using System.ComponentModel.DataAnnotations;

namespace lemossolucoestecnologia.ecommerce.UI.ViewModels.Account
{
    public class LoginViewModel
    {
        public string? Id { get; set; }
        public string? Token { get; set; }
        public string? userName { get; set; }
        public string? password { get; set; }
    }
}
