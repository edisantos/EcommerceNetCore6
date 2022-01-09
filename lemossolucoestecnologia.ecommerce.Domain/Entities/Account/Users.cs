using Microsoft.AspNetCore.Identity;

namespace lemossolucoestecnologia.ecommerce.Domain.Entities.Account
{
    public class Users:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        public ICollection<Sales>? Sales { get; set; }
    }
}
