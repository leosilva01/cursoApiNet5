using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGetCompleteByID : MunicipioTest
    {
        private IMunicipioService _service;

        private Mock<IMunicipioService> _mock;

        [Fact(DisplayName = "É Possível executar metodo Get Complete By Id.")]
        public async Task E_Possivel_Executar_Metodo_GetCompleteById(){
        
            _mock = new Mock<IMunicipioService>();
            _mock.Setup(m => m.GetCompleteById(Id)).ReturnsAsync(dtoCompleto);
            _service = _mock.Object;

            var result = await _service.GetCompleteById(Id);
            Assert.NotNull(result);
            Assert.Equal(result.Id, dto.Id);
            Assert.Equal(result.Nome, dto.Nome);
            Assert.Equal(result.CodIBGE, dto.CodIBGE);
            Assert.Equal(result.UfId, dto.UfId);
            Assert.NotNull(result.Uf);
        }
    }
}