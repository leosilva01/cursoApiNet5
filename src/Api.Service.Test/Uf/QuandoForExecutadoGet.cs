using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Moq;
using Xunit;

namespace Api.Service.Test.Uf
{
    public class QuandoForExecutadoGet : UfTest
    {
        
        private IUfService _service;

        private Mock<IUfService> _mock;

        [Fact(DisplayName = "É Possível executar metodos GET.")]
        public async Task E_Possivel_Executar_Metodo_Get(){

            _mock = new Mock<IUfService>();
            _mock.Setup(m => m.Get(IdUf)).ReturnsAsync(ufDto);
            _service = _mock.Object;

            var result = await _service.Get(IdUf);
            Assert.Equal(result.Id, ufDto.Id);
            Assert.Equal(result.Nome, ufDto.Nome);
            Assert.Equal(result.Sigla, ufDto.Sigla);

            _mock = new Mock<IUfService>();
            _mock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UfDto) null));
            _service = _mock.Object;

            result = await _service.Get(IdUf);
            Assert.Null(result);


        }



    }
}