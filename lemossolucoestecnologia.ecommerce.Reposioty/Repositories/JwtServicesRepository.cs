using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;
using lemossolucoestecnologia.ecommerce.Reposioty.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace lemossolucoestecnologia.ecommerce.Reposioty.Repositories
{
    public class JwtServicesRepository : IAuthenticationServices
    {
        private readonly IConfiguration _config;

        public JwtServicesRepository(IConfiguration config)
        {
            _config = config;
        }
        public string CreateToken(Users users)
        {
            var secret = Encoding.ASCII.GetBytes(_config.GetSection("JwtConfigurations:Secret").Value);
            var key = new SymmetricSecurityKey(secret);
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var securityTokenDcptor = new SecurityTokenDescriptor 
               { 
                  Subject = new ClaimsIdentity(new Claim[]
                  {
                      new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
                      new Claim(ClaimTypes.Name, users.UserName)
                  }),
                  Expires = DateTime.UtcNow.AddHours(4),
                  SigningCredentials = cred,

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = tokenHandler.CreateToken(securityTokenDcptor);
            var token = tokenHandler.WriteToken(tokenGenerated);
            return token;

        }
    }
}
