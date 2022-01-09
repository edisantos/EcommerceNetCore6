using lemossolucoestecnologia.ecommerce.UI.Services;
using lemossolucoestecnologia.ecommerce.UI.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lemossolucoestecnologia.ecommerce.UI.Controllers.Account
{
    public class AccountControlController : Controller
    {
        private readonly ILogger<AcceptedAtActionResult> _logger;
        private readonly IConfiguration _config;
        //private readonly IUsersServices _userServices;
        //private readonly ILoginServices _loginServices;

        public AccountControlController(ILogger<AcceptedAtActionResult> logger, IConfiguration config )
        {
            _logger = logger;
            _config = config;
            //_userServices = userServices;
            //_loginServices = loginServices;
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
        public async Task<IActionResult> Login(LoginInputViewModel mod)
        {
           // var user = await _loginServices.Login(mod);
           // var claims = new List<Claim>
           // {
           //     new Claim(ClaimTypes.NameIdentifier, user.Id),
           //     new Claim("token", user.Token)
           // };

           // var ClamsIdendity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
           // var authProperties = new AuthenticationProperties
           // {
           //     ExpiresUtc = new DateTimeOffset(DateTime.UtcNow.AddHours(2))
           // };
           //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal());
           // ModelState.AddModelError("", $"Usuário: {user.UserName},{user.Token}");
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(UserInputViewModel mod)
        {
            return View();
        }
    }
}
