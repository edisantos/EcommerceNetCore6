using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;

namespace lemossolucoestecnologia.ecommerce.Domain.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public ICollection<Sales>? Sales { get; set; }
       
    }
}
