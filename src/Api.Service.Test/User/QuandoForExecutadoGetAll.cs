using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;


namespace Api.Service.Test.User
{
    public class QuandoForExecutadoGetAll : UserTests
    {
        private IUserService _service;
        private Mock<IUserService> _mock;

        [Fact(DisplayName = "É possível executar método GETALL.")]
        public async Task E_Possivel_Executar_Metodo_GETALL(){
        
            _mock = new Mock<IUserService>();
            _mock.Setup(m => m.GetAll()).ReturnsAsync(userDtoList);
            _service = _mock.Object;
        
            var result = await _service.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() == 10);


            var _listEmpty = new List<UserDto>();

            _mock = new Mock<IUserService>();
            //                                        não entendi o porque desse retorno.
            _mock.Setup(m => m.GetAll()).ReturnsAsync(_listEmpty.AsEnumerable);
            _service = _mock.Object;

            var resultListEmpty = await _service.GetAll();
            Assert.Empty(resultListEmpty);
            Assert.True(resultListEmpty.Count() == 0);
        }
    }
}