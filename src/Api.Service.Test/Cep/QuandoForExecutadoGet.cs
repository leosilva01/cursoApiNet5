using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Moq;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoGet : CepTest
    {
        private ICepService _service;

        private Mock<ICepService> _mock;

        [Fact(DisplayName = "É Possível executar metodo Get.")]
        public async Task E_Possivel_Executar_Metodo_Get(){
        
            _mock = new Mock<ICepService>();
            _mock.Setup(m => m.Get(Id)).ReturnsAsync(dto);
            _service = _mock.Object;

            var result = await _service.Get(Id);
            Assert.NotNull(result);
            Assert.Equal(result.Cep, Cep);
            Assert.Equal(result.Logradouro, Logradouro);
            Assert.Equal(result.MunicipioId, MunicipioId);
            Assert.Equal(result.Numero, Numero);
            Assert.NotNull(result.Municipio);

            _mock = new Mock<ICepService>();
            _mock.Setup(m => m.Get(Cep)).ReturnsAsync(dto);
            _service = _mock.Object;

            result = await _service.Get(Cep);
            Assert.NotNull(result);
            Assert.Equal(result.Cep, Cep);
            Assert.Equal(result.Logradouro, Logradouro);
            Assert.Equal(result.MunicipioId, MunicipioId);
            Assert.Equal(result.Numero, Numero);
            Assert.NotNull(result.Municipio);


            _mock = new Mock<ICepService>();
            _mock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync((CepDto)null);
            _service = _mock.Object;

            result = await _service.Get(Id);
            Assert.Null(result);

        }
    }
}