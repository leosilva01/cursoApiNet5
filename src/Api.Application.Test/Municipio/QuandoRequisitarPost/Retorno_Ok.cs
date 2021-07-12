using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarPost
{
    public class Retorno_Ok
    {

        MunicipiosController _controller;

        [Fact(DisplayName = "É Possivel Realizar o Post.")]
        public async Task E_Possivel_Invocar_a_Controller_Post(){
            
            var serviceMock = new Mock<IMunicipioService>();

            serviceMock.Setup(u => u.Post(It.IsAny<MunicipioDtoCreate>())).ReturnsAsync(
                new MunicipioDtoCreateResult{
                    Id = Guid.NewGuid(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    Nome = "Brasília",
                    UfId = Guid.NewGuid(),
                    CreateAt = DateTime.Now
                }
            );

            _controller = new MunicipiosController(serviceMock.Object);

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var result = await _controller.Post(new MunicipioDtoCreate{
                Nome = "Brasília",
                CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = Guid.NewGuid()
            });

            Assert.True(result is CreatedResult);
        }
    }
}