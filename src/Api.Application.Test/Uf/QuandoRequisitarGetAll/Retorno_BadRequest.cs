using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.Uf;
using Api.Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Uf.QuandoRequisitarGetAll
{
    public class Retorno_BadRequest
    {
        private UfsController _controller;

        [Fact(DisplayName = "É Possivel Realizar o GetAll.")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll(){
            
            var serviceMock = new Mock<IUfService>();

            List<UfDto> dtoList = new List<UfDto>();

            dtoList.Add(new UfDto{
                Id = Guid.NewGuid(),
                Nome = "Distrito Federal",
                Sigla = "DF",
            });

            dtoList.Add(new UfDto{
                Id = Guid.NewGuid(),
                Nome = "São Paulo",
                Sigla = "SP",
            });

            serviceMock.Setup(u => u.GetAll()).ReturnsAsync(dtoList);

            _controller = new UfsController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }
    }
}