using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.Cep;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoDelete : CepTest
    {
        private ICepService _service;

        private Mock<ICepService> _mock;

        [Fact(DisplayName = "É Possível executar metodo Delete.")]
        public async Task E_Possivel_Executar_Metodo_Delete(){
        
            _mock = new Mock<ICepService>();
            _mock.Setup(m => m.Delete(Id)).ReturnsAsync(true);
            _service = _mock.Object;

            var result = await _service.Delete(Id);
        }
    }
}