using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class QuandoForExecutadoUpdate : UserTests
    {
        private IUserService _service;
        private Mock<IUserService> _mock;

        
        [Fact(DisplayName = "É possível executar método UPDATE.")]
        public async Task E_Possivel_Executar_Metodo_Update(){
        
            _mock = new Mock<IUserService>();
            _mock.Setup(m => m.Put(userDtoUpdate)).ReturnsAsync(userDtoUpdateResult);
            _service = _mock.Object;

            var result = await _service.Put(userDtoUpdate);
            Assert.NotNull(result);
            Assert.True(result.Id == idUser);
            Assert.True(result.Name == nomeAlterado);
            Assert.True(result.Email == emailAlterado);
        }
    }
}