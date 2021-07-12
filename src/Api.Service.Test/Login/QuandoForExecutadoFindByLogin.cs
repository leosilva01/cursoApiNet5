using System;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.Login;
using Moq;
using Xunit;

namespace Api.Service.Test.Login
{
    public class QuandoForExecutadoFindByLogin
    {
        private ILoginService _service;
        private Mock<ILoginService> _mock;

        [Fact(DisplayName = "É Possível Executar o Método FindByLogin")]
        public async Task E_Possivel_Executar_Metodo_FindByLogin()
        {
            var email = Faker.Internet.Email();
            var objetoRetorno = new 
            {
                authenticated = true,
                created = DateTime.UtcNow,
                expiration = DateTime.UtcNow.AddHours(8),
                accessToken = Guid.NewGuid(),
                userName = Faker.Internet.Email(),
                message = "Usuário Logado com sucesso."
            };

            var loginDto = new LoginDto{
                Email = email
            };

            _mock = new Mock<ILoginService>();
            _mock.Setup(m => m.FindByLogin(loginDto)).ReturnsAsync(objetoRetorno);
            _service = _mock.Object;

            var result = await _service.FindByLogin(loginDto);
            Assert.NotNull(result);
        }
    }
}