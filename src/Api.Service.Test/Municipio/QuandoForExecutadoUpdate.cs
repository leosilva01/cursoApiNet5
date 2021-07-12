using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoUpdate : MunicipioTest
    {
        private IMunicipioService _service;

        private Mock<IMunicipioService> _mock;

        [Fact(DisplayName = "É Possível executar metodo Update.")]
        public async Task E_Possivel_Executar_Metodo_Update(){
        
            _mock = new Mock<IMunicipioService>();
            _mock.Setup(m => m.Put(dtoUpdate)).ReturnsAsync(dtoUpdateResult);
            _service = _mock.Object;

            var resultUpdate = await _service.Put(dtoUpdate);
            Assert.NotNull(resultUpdate);
            Assert.Equal(NomeAlterado, resultUpdate.Nome);
            Assert.Equal(CodIBGEAlterado, resultUpdate.CodIBGE);
            Assert.Equal(UfId, resultUpdate.UfId);
        }
    }
}