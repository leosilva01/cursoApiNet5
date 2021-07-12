using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Cep.QuandoRequistarGetByCep
{
    public class Retorno_NotFound
    {
        CepsController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Get by Cep.")]
        public async Task E_Possivel_Invocar_a_Controller_Get_by_Cep(){
            
            var serviceMock = new Mock<ICepService>();

            serviceMock.Setup(u => u.Get(It.IsAny<string>())).Returns(Task.FromResult((CepDto) null));

            _controller = new CepsController(serviceMock.Object);

            var result = await _controller.Get(It.IsAny<string>());

            Assert.True(result is NotFoundResult);
        }
    }
}