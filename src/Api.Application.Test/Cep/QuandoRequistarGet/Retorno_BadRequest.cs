using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.QuandoRequistarGet
{
    public class Retorno_BadRequest
    {
        CepsController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Get.")]
        public async Task E_Possivel_Invocar_a_Controller_Get(){
            
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(u => u.Get(It.IsAny<Guid>())).ReturnsAsync(
                new CepDto{
                    Id = Guid.NewGuid(),
                }
            );

            _controller = new CepsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Cep", "É campo Obrigatório.");

            var result = await _controller.Get(Guid.NewGuid());

            Assert.True(result is BadRequestObjectResult);
        }
    }
}