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
    public class Retorno_NotFound
    {
        CepsController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Put.")]
        public async Task E_Possivel_Invocar_a_Controller_Put(){
            
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(u => u.Put(It.IsAny<CepDtoUpdate>())).Returns(Task.FromResult((CepDtoUpdateResult) null));

            _controller = new CepsController(serviceMock.Object);

            var result = await _controller.Put(It.IsAny<CepDtoUpdate>());

            Assert.True(result is NotFoundResult);
        }
    }
}