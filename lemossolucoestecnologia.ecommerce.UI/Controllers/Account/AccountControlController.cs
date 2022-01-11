using lemossolucoestecnologia.ecommerce.UI.Services;
using lemossolucoestecnologia.ecommerce.UI.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Security.Claims;

namespace lemossolucoestecnologia.ecommerce.UI.Controllers.Account
{
    public class AccountControlController : Controller
    {
        private readonly ILogger<AccountControlController> _logger;
        private readonly IUsersServices _userServices;
        private readonly ILoginServices _loginServices;

        public AccountControlController(ILogger<AccountControlController> logger, 
            IUsersServices userServices, 
            ILoginServices loginServices)
        {
           _logger = logger;
           _userServices = userServices;
            _loginServices = loginServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel mod)
        {
            try
            {
                var user = await _loginServices.Login(mod);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim("token", user.Token)

                };

                var ClamsIdendity = new ClaimsIdentity(claims, 
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = new DateTimeOffset(DateTime.UtcNow.AddHours(2))
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal());
                TempData["Success"] =  $"Usuário: {user.userName},{user.Token}";
                return RedirectToAction(nameof(Index));
                //ModelState.AddModelError("", $"Usuário: {user.UserName},{user.Token}");

            }
            catch (ApiException ex)
            {

                ModelState.AddModelError(String.Empty, $"Erro na api: {ex.Message}");
            }

            catch (Exception ex)
            {

                ModelState.AddModelError(String.Empty, $"Erro : {ex.Message}");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserInputViewModel mod)
        {
            try
            {
               await _userServices.Register(mod);
                TempData["Success"] = "Registro realizado com sucesso!";
                return RedirectToAction(nameof(Register));
            }
            catch (ApiException ex)
            {

                ModelState.AddModelError(String.Empty, $"Ops, houve um erro: {ex.Message}");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(String.Empty, $"Ops, houve um erro: {ex.Message}");
            }
            return View();
        }
    }
}
