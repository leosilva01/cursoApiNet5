using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarDelete
{
    public class Retorno_Ok
    {
        MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Delete.")]
        public async Task E_Possivel_Invocar_a_Controller_Delete(){
            
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(u => u.Delete(It.IsAny<Guid>()))
                       .ReturnsAsync(true);

            _controller = new MunicipiosController(serviceMock.Object);

            var result = await _controller.Delete(It.IsAny<Guid>());
            Assert.True(result is OkObjectResult);
        }
    }
}