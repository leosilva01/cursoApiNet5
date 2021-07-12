using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class QuandoForExecutadoDelete : UserTests
    {
        
        private IUserService _service;
        private Mock<IUserService> _mock;

        
        [Fact(DisplayName = "É possível executar método UPDATE.")]
        public async Task E_Possivel_Executar_Metodo_Update(){
        
            _mock = new Mock<IUserService>();
            _mock.Setup(m => m.Delete(idUser)).ReturnsAsync(true);
            _service = _mock.Object;

            var result = await _service.Delete(idUser);
            Assert.True(result);

            _mock = new Mock<IUserService>();
            _mock.Setup(m => m.Delete(idUser)).ReturnsAsync(false);
            _service = _mock.Object;

            result = await _service.Delete(Guid.NewGuid());
            Assert.False(result);
        }
    }
}