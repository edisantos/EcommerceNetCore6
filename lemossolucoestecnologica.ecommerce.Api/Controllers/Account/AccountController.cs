using AutoMapper;
using lemossolucoestecnologia.ecommerce.Data.Contexto;
using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;
using lemossolucoestecnologia.ecommerce.Reposioty.Interface;
using lemossolucoestecnologica.ecommerce.Api.ErrorsValidation;
using lemossolucoestecnologica.ecommerce.Api.Filters;
using lemossolucoestecnologica.ecommerce.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace lemossolucoestecnologica.ecommerce.Api.Controllers.Account
{
    [Route("api/v1/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IUserClaimsPrincipalFactory<Users> _userClaimsPrincipalFactory;
        private readonly DataContext _context;
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IAuthenticationServices _authenticationServices;

        //private readonly IMapper _mapper;

        public AccountController(UserManager<Users> userManager,
            SignInManager<Users> signInManager,
           IUserClaimsPrincipalFactory<Users> userClaimsPrincipalFactory,
           DataContext context,
           IUserServices userServices,
           IMapper mapper,
           IConfiguration config,
           IAuthenticationServices authenticationServices

         )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _context = context;
            _userServices = userServices;
            _mapper = mapper;
            _config = config;
            _authenticationServices = authenticationServices;
        }

        [SwaggerResponse(statusCode: 200, description: "Autenticado com sucesso", type: typeof(LoginViewModel))]
        [SwaggerResponse(statusCode: 400, description: "Campos Obrigatórios", type: typeof(ValidateFieldViewModel))]
        [SwaggerResponse(statusCode: 500, description: "Erro no servidor", type: typeof(GenericErrorViewModel))]
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ValidateModelStateConstumer]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false);
            if (result.Succeeded)
            {
                var appUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.UserName == loginViewModel.UserName);

                var userToReturn = _mapper.Map<LoginViewModel>(appUser);
                var tokenService = _authenticationServices.CreateToken(appUser); //refactored code
                return Ok(new
                {
                    //token = GeneratejwtToken(appUser).Result,
                    token = tokenService,
                    user = userToReturn
                });


            }

            return Unauthorized();



        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<string> GeneratejwtToken(Users users)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
                new Claim(ClaimTypes.Name, users.UserName)
            };

            var roles = await _userManager.GetRolesAsync(users);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.ASCII
                 .GetBytes(_config.GetSection("JwtConfigurations:Secret").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = cred,

            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }


        [SwaggerResponse(statusCode: 201, description: "Usuário registrado com sucesso", type: typeof(UsersViewModels))]
        [SwaggerResponse(statusCode: 401, description: "Não Autorizado", type: typeof(ValidateFieldViewModel))]
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Regiter(UsersViewModels mod)
        {
            var userMapp = _mapper.Map<Users>(mod);
            var user = await _userManager.FindByNameAsync(mod.UserName);

            try
            {
                var result = await _userManager.CreateAsync(userMapp, mod.Password);
                var userToReturn = _mapper.Map<UsersViewModels>(userMapp);
                if (result.Succeeded)
                {
                    return Created("", userToReturn);
                    //var token = await _userManager.GenerateEmailConfirmationTokenAsync(userMapp);
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados falhou {ex.Message}");
            }






        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userServices.GetAll();
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result.ToList());

        }

        [HttpDelete]
        [Route("RemoveUser/{Id}")]
        public async Task<IActionResult> RemoveUser(string Id)
        {
            var user = _context.Users.Where(x => x.Id == Id).FirstOrDefault();
            if (user == null)
            {
                BadRequest("Nenhum usuário encontrado");

            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok("Usuario Removido com sucesso");
        }
    }
}
