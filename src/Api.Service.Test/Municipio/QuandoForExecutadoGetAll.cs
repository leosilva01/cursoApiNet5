using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Moq;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoGetAll : MunicipioTest
    {
        private IMunicipioService _service;

        private Mock<IMunicipioService> _mock;

        [Fact(DisplayName = "É Possível executar metodo GetAll.")]
        public async Task E_Possivel_Executar_Metodo_GetAll(){
        
            _mock = new Mock<IMunicipioService>();
            _mock.Setup(m => m.GetAll()).ReturnsAsync(dtoList);
            _service = _mock.Object;

            var result = await _service.GetAll();
            Assert.True(result.Count() == 10);

            
            //List vazia
            _mock = new Mock<IMunicipioService>();
            _mock.Setup(m => m.GetAll()).ReturnsAsync(new List<MunicipioDto>().AsEnumerable);
            _service = _mock.Object;

            var resultEmpty = await _service.GetAll();
            Assert.Empty(resultEmpty);
        }
    }
}