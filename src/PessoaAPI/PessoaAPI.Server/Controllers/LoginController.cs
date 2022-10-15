using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PessoaAPI.Data;
using PessoaAPI.Service;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UPDown.Common.PessoaAPI;

namespace PessoaAPI.Server.Controllers
{
    // Todo:
    // Melhorar o microserviço para ser mais genérico

    [ApiController]
    [Route("[controller]")]
    public class LoginController
    {
        private readonly ILogger<LoginController> _logger;
        private readonly DatabaseContext _dbContext;

        public LoginController(ILogger<LoginController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{email}/{senha}")]
        public LoginResultDTO Get([FromRoute] string email, [FromRoute] string senha)
        {
            LoginResultDTO result = null;
            DateTime expires = DateTime.Now.AddMinutes(480);

            if (email == "admin" && senha == "admin")
            {
                LoginResultDTO login = new LoginResultDTO
                {
                    Token = GenerateJWT(email, UserType.Admin, expires),
                    Expiry = expires,
                    User = UserType.Admin
                };
                return login;
            }

            var aluno = new AlunoService(_dbContext).Get(x => x.Email == email && x.Senha == senha);

            if (aluno != null)
            {
                result = new LoginResultDTO
                {
                    Token = GenerateJWT(email, UserType.Aluno, expires),
                    Expiry = expires,
                    User = UserType.Aluno
                };
            }
            else
            {
                var professor = new ProfessorService(_dbContext).Get(x => x.Email == email && x.Senha == senha);

                if (professor != null)
                {
                    result = new LoginResultDTO
                    {
                        Token = GenerateJWT(email, UserType.Professor, expires),
                        Expiry = expires,
                        User = UserType.Professor
                    };
                }
            }

            return result;
        }

        private string GenerateJWT(string email, UserType userType, DateTime expires)
        {
            try
            {
                SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(AuthenticationDTO.jwtKey));
                JwtSecurityToken token = new(
                    AuthenticationDTO.jwtIssuer,
                    AuthenticationDTO.jwtAudience,
                    new[] {
                        new Claim(ClaimTypes.Name, email),
                        new Claim("UserType", userType.ToString())
                    },
                    expires: expires,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );
                JwtSecurityTokenHandler tokenHandler = new();

                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
