using System.ComponentModel.DataAnnotations;

namespace lemossolucoestecnologica.ecommerce.Api.ViewModels
{
    public class UsersViewModels
    {
        [Display(Name ="Primeiro Nome")]
        [Required(ErrorMessage ="O Primeiro nome é obrigatório")]
        public string? FirstName { get; set; }
        [Display(Name = "Último Nome")]
        [Required(ErrorMessage = "O Último nome é obrigatório")]
        public string? LastName { get; set; }
        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "O Endereço é obrigatório")]
        public string? Address { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [EmailAddress(ErrorMessage ="E-mail invalido")]
        public string? Email { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O Usuário é obrigatório")]
        [StringLength(30, ErrorMessage ="Quantidade maximo {0}", MinimumLength =6)]
        public string? UserName { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Confirmar Senha")]
        [Required(ErrorMessage = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="As Senhas não conferem")]
        public string? ConfirmPassword { get; set; }


    }
}
