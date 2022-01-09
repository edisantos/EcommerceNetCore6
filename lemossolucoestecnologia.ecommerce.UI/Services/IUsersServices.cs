using lemossolucoestecnologia.ecommerce.UI.ViewModels.Account;
using Refit;

namespace lemossolucoestecnologia.ecommerce.UI.Services
{
    public interface IUsersServices
    {
        [Post("/v1/Account/Register")]
        Task Register(UserInputViewModel model);
    }
}
