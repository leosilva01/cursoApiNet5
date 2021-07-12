using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarPut
{
    public class Retorno_BadRequest
    {

        MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Put.")]
        public async Task E_Possivel_Invocar_a_Controller_Put(){
            
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(u => u.Put(It.IsAny<MunicipioDtoUpdate>())).ReturnsAsync(
                new MunicipioDtoUpdateResult{
                    Id = Guid.NewGuid(),
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Nome", "É Campo Obrigatório.");

            var result = await _controller.Put(It.IsAny<MunicipioDtoUpdate>());
            Assert.True(result is BadRequestObjectResult);
        }
    }
}