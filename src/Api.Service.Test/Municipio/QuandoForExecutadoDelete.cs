using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoDelete : MunicipioTest
    {
        private IMunicipioService _service;

        private Mock<IMunicipioService> _mock;

        [Fact(DisplayName = "É Possível executar metodo Delete.")]
        public async Task E_Possivel_Executar_Metodo_Delete(){
        
            _mock = new Mock<IMunicipioService>();
            _mock.Setup(m => m.Delete(Id)).ReturnsAsync(true);
            _service = _mock.Object;

            var result = await _service.Delete(Id);
        }
    }
}