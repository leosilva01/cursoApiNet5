using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.QuandoRequisitarGetAll
{
    public class Retorno_GetAll_BadRequest
    {
        
        private UsersController _usersController;

        [Fact(DisplayName = "É Possível Realizar o BadRequest no Método GetAll.")]
        public async Task E_Possivel_Invocar_a_Controller_Get_BadRequest(){

            var mock = new Mock<IUserService>();
        
            mock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<UserDto>
                {
                    new UserDto {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                    },
                    new UserDto {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                    }
                });

            _usersController = new UsersController(mock.Object);
            _usersController.ModelState.AddModelError("Id", "Formato Inválido.");

            var result = await _usersController.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }
    }
}