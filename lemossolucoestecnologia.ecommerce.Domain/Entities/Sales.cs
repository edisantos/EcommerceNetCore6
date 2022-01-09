using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;

namespace lemossolucoestecnologia.ecommerce.Domain.Entities
{
    public class Sales
    {
        public int Id { get; set; }
        public DateTime DateSales { get; set; }
        public string? UserId  { get; set; }
        public int ProductId { get; set; }  
        public virtual Users? Users { get; set; }
        public virtual Products? Products { get; set; }
    }
}
