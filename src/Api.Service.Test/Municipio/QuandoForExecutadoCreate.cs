using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoCreate : MunicipioTest
    {
        private IMunicipioService _service;

        private Mock<IMunicipioService> _mock;

        [Fact(DisplayName = "É Possível executar metodo Create.")]
        public async Task E_Possivel_Executar_Metodo_Create(){
        
            _mock = new Mock<IMunicipioService>();
            _mock.Setup(m => m.Post(dtoCreate)).ReturnsAsync(dtoCreateResult);
            _service = _mock.Object;

            var result = await _service.Post(dtoCreate);
            Assert.NotNull(result);
            Assert.Equal(Nome, result.Nome);
            Assert.Equal(CodIBGE, result.CodIBGE);
            Assert.Equal(UfId, result.UfId);
        }

    }
}