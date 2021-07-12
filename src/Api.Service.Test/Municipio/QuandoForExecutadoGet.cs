using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGet :MunicipioTest
    {
        private IMunicipioService _service;

        private Mock<IMunicipioService> _mock;

        [Fact(DisplayName = "É Possível executar metodo Get.")]
        public async Task E_Possivel_Executar_Metodo_Get(){
        
            _mock = new Mock<IMunicipioService>();
            _mock.Setup(m => m.Get(Id)).ReturnsAsync(dto);
            _service = _mock.Object;

            var result = await _service.Get(Id);
            Assert.NotNull(result);
            Assert.Equal(result.Id, dto.Id);
            Assert.Equal(result.Nome, dto.Nome);
            Assert.Equal(result.CodIBGE, dto.CodIBGE);
            Assert.Equal(result.UfId, dto.UfId);

            _mock = new Mock<IMunicipioService>();
            _mock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((MunicipioDto)null));
            _service = _mock.Object;

            var resultFalse = await _service.Get(Guid.NewGuid());
            Assert.Null(resultFalse);
        }
    }
}