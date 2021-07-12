using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoUpdate : CepTest
    {
        private ICepService _service;

        private Mock<ICepService> _mock;

        [Fact(DisplayName = "É Possível executar metodo Update.")]
        public async Task E_Possivel_Executar_Metodo_Update(){
        
            _mock = new Mock<ICepService>();
            _mock.Setup(m => m.Put(dtoUpdate)).ReturnsAsync(dtoUpdateResult);
            _service = _mock.Object;

            var result = await _service.Put(dtoUpdate);
            Assert.NotNull(result);
            Assert.Equal(result.Id, Id);
            Assert.Equal(result.Cep, CepAlterado);
            Assert.Equal(result.Logradouro, LogradouroAlterado);
            Assert.Equal(result.MunicipioId, MunicipioId);
            Assert.Equal(result.Numero, NumeroAlterado);

            _mock = new Mock<ICepService>();
            _mock.Setup(m => m.Put(It.IsAny<CepDtoUpdate>())).ReturnsAsync((CepDtoUpdateResult)null);
            _service = _mock.Object;

            result = await _service.Put(dtoUpdate);
            Assert.Null(result);
        }
    }
}