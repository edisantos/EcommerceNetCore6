using System.ComponentModel.DataAnnotations;

namespace lemossolucoestecnologia.ecommerce.UI.ViewModels.Account
{
    public class LoginInputViewModel
    {
        public string? Id { get; set; }
        public string? Token { get; set; }
        [Display(Name ="Usuário")]
        [Required]
        public string? UserName { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }
    }
}
