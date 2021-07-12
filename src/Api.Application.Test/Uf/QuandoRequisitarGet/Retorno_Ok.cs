using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Uf.QuandoRequisitarGet
{
    public class Retorno_Ok
    {
        private UfsController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Get.")]
        public async Task E_Possivel_Invocar_a_Controller_Get(){
            
            var serviceMock = new Mock<IUfService>();

            serviceMock.Setup(u => u.Get(It.IsAny<Guid>())).ReturnsAsync(
                new UfDto{
                    Id = Guid.NewGuid(),
                    Nome = "Brasília",
                    Sigla = "DF"
                }
            );

            _controller = new UfsController(serviceMock.Object);

            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);

        }
    }
}