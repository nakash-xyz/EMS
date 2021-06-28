using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.BAL
{
    public class AccountBAL : IAccountBAL
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        
        public AccountBAL(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecretKey"]));
        }

        public Task<UserDTO> Login(string username, string password)
        {
            var userRole = string.Empty;

            if (username.ToLower() == "alice" && password == "Password@1")
            {
                userRole = "Admin";
            }
            else if (username.ToLower() == "bob" && password == "Password@1")
            {
                userRole = "User";
            }
            else
            {
                throw new Exception("Invalid Login Attempt!");
            }

            var displayName = userRole == "Admin" ? "Alice" : "Bob";

            var roleClaim = new Claim(ClaimTypes.Role, userRole);
            var nameClaim = new Claim("displayName", displayName);

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Token:Issuer"],
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials,
                Subject = new ClaimsIdentity(new Claim[] { roleClaim, nameClaim })
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = new UserDTO
            {
                Token = tokenHandler.WriteToken(token),
                DisplayName = userRole == "Admin" ? "Alice" : "Bob"
            };

            return Task.FromResult(user);
        }
    }
}