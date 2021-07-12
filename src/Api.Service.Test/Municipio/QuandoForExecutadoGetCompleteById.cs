using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGetCompleteByIBGE : MunicipioTest
    {
        private IMunicipioService _service;

        private Mock<IMunicipioService> _mock;

        [Fact(DisplayName = "É Possível executar metodo Get Complete By IBGE.")]
        public async Task E_Possivel_Executar_Metodo_GetCompleteByIBGE(){
        
            _mock = new Mock<IMunicipioService>();
            _mock.Setup(m => m.GetCompleteByIBGE(CodIBGE)).ReturnsAsync(dtoCompleto);
            _service = _mock.Object;

            var result = await _service.GetCompleteByIBGE(CodIBGE);
            Assert.NotNull(result);
            Assert.Equal(result.Id, dto.Id);
            Assert.Equal(result.Nome, dto.Nome);
            Assert.Equal(result.CodIBGE, dto.CodIBGE);
            Assert.Equal(result.UfId, dto.UfId);
            Assert.NotNull(result.Uf);
        }
    }
}