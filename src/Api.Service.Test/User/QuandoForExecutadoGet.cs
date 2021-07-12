using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class QuandoForExecutadoGet : UserTests
    {
        private IUserService _service;
        private Mock<IUserService> _mock;

        
        [Fact(DisplayName = "É possível executar método GET.")]
        public async Task E_Possivel_Executar_Metodo_GET(){

            _mock = new Mock<IUserService>();
            _mock.Setup(m => m.Get(idUser)).ReturnsAsync(userDto);
            _service = _mock.Object;

            var result = await _service.Get(idUser);
            Assert.NotNull(result);
            Assert.True(result.Id == idUser);
            Assert.True(result.Name == nameUser);
            Assert.True(result.Email == emailUser);

            _mock = new Mock<IUserService>();
            _mock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDto) null));
            _service = _mock.Object;

            var _record = await _service.Get(idUser);
            Assert.True(_record == null);
        }
    }
}