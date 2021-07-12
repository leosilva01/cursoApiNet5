using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarGetById
{
    public class Retorno_BadRequest
    {

        MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Get by ID.")]
        public async Task E_Possivel_Invocar_a_Controller_Get_by_Id(){
            
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(u => u.GetCompleteById(It.IsAny<Guid>())).ReturnsAsync(
                new MunicipioDtoCompleto{
                    Id = Guid.NewGuid(),
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);
            _controller.ModelState.AddModelError("Nome", "É Campo é Obrigatório.");

            var result = await _controller.GetCompleteById(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);
        }
    }
}