using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class QuandoForExecutadoCreate : UserTests
    {

        private IUserService _service;
        private Mock<IUserService> _mock;

        
        [Fact(DisplayName = "É possível executar método CREATE.")]
        public async Task E_Possivel_Executar_Metodo_Create(){
        
            _mock = new Mock<IUserService>();
            _mock.Setup(m => m.Post(userDtoCreate)).ReturnsAsync(userDtoCreateResult);
            _service = _mock.Object;

            var result = await _service.Post(userDtoCreate);
            Assert.NotNull(result);
            Assert.True(result.Id == idUser);
            Assert.True(result.Name == nameUser);
            Assert.True(result.Email == emailUser);
        }
    }
}