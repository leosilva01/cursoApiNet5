using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.QuandoRequistarPut
{
    public class Retorno_BadRequest
    {
        CepsController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Put.")]
        public async Task E_Possivel_Invocar_a_Controller_Put(){
            
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(u => u.Put(It.IsAny<CepDtoUpdate>())).ReturnsAsync(
                new CepDtoUpdateResult{
                    Id = Guid.NewGuid(),
                }
            );

            _controller = new CepsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Cep", "É campo Obrigatório.");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var result = await _controller.Put(It.IsAny<CepDtoUpdate>());

            Assert.True(result is BadRequestObjectResult);
        }
    }
}