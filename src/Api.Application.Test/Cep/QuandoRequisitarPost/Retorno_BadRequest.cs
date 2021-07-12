using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.QuandoRequistarPost
{
    public class Retorno_BadRequest
    {
        CepsController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Post.")]
        public async Task E_Possivel_Invocar_a_Controller_Post(){
            
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(u => u.Post(It.IsAny<CepDtoCreate>())).ReturnsAsync(
                new CepDtoCreateResult{
                    Id = Guid.NewGuid(),
                }
            );

            _controller = new CepsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Cep", "É campo Obrigatório.");

            var result = await _controller.Post(It.IsAny<CepDtoCreate>());

            Assert.True(result is BadRequestObjectResult);
        }
    }
}