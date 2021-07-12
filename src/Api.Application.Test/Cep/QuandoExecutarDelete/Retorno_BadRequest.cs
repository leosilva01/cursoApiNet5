using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.QuandoRequistarDelete
{
    public class Retorno_Delete
    {
        CepsController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Delete.")]
        public async Task E_Possivel_Invocar_a_Controller_Delete(){
            
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(u => u.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new CepsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Cep", "É campo Obrigatório.");

            var result = await _controller.Delete(It.IsAny<Guid>());

            Assert.True(result is BadRequestObjectResult);
        }
    }
}