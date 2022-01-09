using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;

namespace lemossolucoestecnologia.ecommerce.Reposioty.Interface
{
    public interface IAuthenticationServices
    {
        string CreateToken(Users users);
    }
}
