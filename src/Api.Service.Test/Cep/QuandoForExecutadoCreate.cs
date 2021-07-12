using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoCreate : CepTest
    {
        private ICepService _service;

        private Mock<ICepService> _mock;

        [Fact(DisplayName = "É Possível executar metodo Create.")]
        public async Task E_Possivel_Executar_Metodo_Create(){
        
            _mock = new Mock<ICepService>();
            _mock.Setup(m => m.Post(dtoCreate)).ReturnsAsync(dtoCreateResult);
            _service = _mock.Object;

            var result = await _service.Post(dtoCreate);
            Assert.NotNull(result);
            Assert.Equal(result.Id, Id);
            Assert.Equal(result.Logradouro, Logradouro);
            Assert.Equal(result.Cep, Cep);
            Assert.Equal(result.MunicipioId, MunicipioId);
            Assert.Equal(result.Numero, Numero);

            _mock = new Mock<ICepService>();
            _mock.Setup(m => m.Post(It.IsAny<CepDtoCreate>())).ReturnsAsync((CepDtoCreateResult)null);
            _service = _mock.Object;

            result = await _service.Post(dtoCreate);
            Assert.Null(result);
        }
    }
}