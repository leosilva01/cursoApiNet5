using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarGetByIBGE
{
    public class Retorno_BadRequest
    {

        MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Get by IBGE.")]
        public async Task E_Possivel_Invocar_a_Controller_Get_by_IBGE(){
            
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(u => u.GetCompleteByIBGE(It.IsAny<int>())).ReturnsAsync(
                new MunicipioDtoCompleto{
                    Id = Guid.NewGuid(),
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Nome", "Nome é Obrigatório.");

            var result = await _controller.GetCompleteByIBGE(It.IsAny<int>());
            Assert.True(result is BadRequestObjectResult);
        }
    }
}