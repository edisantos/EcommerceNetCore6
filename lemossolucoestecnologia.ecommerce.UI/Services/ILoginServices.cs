using lemossolucoestecnologia.ecommerce.UI.ViewModels.Account;
using Refit;

namespace lemossolucoestecnologia.ecommerce.UI.Services
{
    public interface ILoginServices
    {
        [Post("/v1/Account/Login")]
        Task<LoginViewModel> Login(LoginViewModel mod);
    }
}
