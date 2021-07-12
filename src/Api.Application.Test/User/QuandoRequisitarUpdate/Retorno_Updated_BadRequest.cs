using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.QuandoRequisitarUpdate
{
    public class Retorno_Updated_BadRequest
    {
        private UsersController _usersController;

        [Fact(DisplayName = "É Possível Realizar o BadRequest Updated.")]
        public async Task E_Possivel_Invocar_a_Controller_Update_BadRequest(){

            var mock = new Mock<IUserService>();
            var id = Guid.NewGuid();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            mock.Setup(m => m.Put(It.IsAny<UserDtoUpdate>())).ReturnsAsync(
                new UserDtoUpdateResult{
                    Id = id,
                    Name = nome,
                    Email = email,
                    UpdateAt = DateTime.UtcNow
                });

                _usersController = new UsersController(mock.Object);
            _usersController.ModelState.AddModelError("Email", "E-mail é um campo Obrigatório.");

            var userDtoUpdate = new UserDtoUpdate{
                Id = id,
                Name = nome,
                Email = email
            };

            var result = await _usersController.Put(userDtoUpdate);
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_usersController.ModelState.IsValid);
        }
    }
}