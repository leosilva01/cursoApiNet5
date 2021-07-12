using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Login;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;

        private SigningConfigurations _signingConfigurations;

        private IConfiguration _configuration{ get; }

        public LoginService(IUserRepository repository, 
                            SigningConfigurations signingConfigurations, 
                            IConfiguration configuration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _configuration = configuration;
        }
        public async Task<object> FindByLogin(LoginDto user)
        {

            var result = new UserEntity();

            if(user != null && !string.IsNullOrWhiteSpace(user.Email)) {
                
                result = await _repository.FindByLogin(user.Email);

                if(result == null){

                    return new {
                        authenticated = false,
                        message = "Falha ao Autenticar."
                    };

                } else {

                    var identity = new ClaimsIdentity(
                        new GenericIdentity(result.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, result.Email),

                        }
                    );

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds")));

                    var handler = new JwtSecurityTokenHandler();

                    string token = CreateToken(identity,createDate,expirationDate,handler);

                    return SuccessObject(createDate, expirationDate, token, user);


                }

            }else{

                return "Falha ao Autenticar.";
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler){
            
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                Audience = Environment.GetEnvironmentVariable("Audience"),
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            string token = handler.WriteToken(securityToken);

            return token;
        }
        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user)
        {
            return new {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = user.Email,
                message = "Usuário Logado com sucesso."
            };
        }
    }

}