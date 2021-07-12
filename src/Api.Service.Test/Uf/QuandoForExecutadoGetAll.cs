using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Moq;
using Xunit;

namespace Api.Service.Test.Uf
{
    public class QuandoForExecutadoGetAll : UfTest
    {
        
        private IUfService _service;

        private Mock<IUfService> _mock;

        [Fact(DisplayName = "É Possível executar metodos GET All.")]
        public async Task E_Possivel_Executar_Metodo_GetAll(){

            _mock = new Mock<IUfService>();
            _mock.Setup(m => m.GetAll()).ReturnsAsync(ufDtoList);
            _service = _mock.Object;

            var result = await _service.GetAll();
            Assert.True(result.Count() == ufDtoList.Count());


            List<UfDto> listResult = new List<UfDto>();

            _mock = new Mock<IUfService>();
            _mock.Setup(m => m.GetAll()).ReturnsAsync(listResult.AsEnumerable);
            _service = _mock.Object;

            result = await _service.GetAll();
            Assert.Empty(result);


        }



    }
}