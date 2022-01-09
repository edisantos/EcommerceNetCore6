using System.ComponentModel.DataAnnotations;

namespace lemossolucoestecnologica.ecommerce.Api.ViewModels
{
    public class ProductsViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Produto")]
        [Required]
        public string? ProductName { get; set; }
        [Display(Name = "Preço")]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Descrição")]
        [Required]
        public string? Description { get; set; }
    }
}
