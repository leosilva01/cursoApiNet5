using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.QuandoRequisitarCreate
{
    public class Retorno_Created_BadRequest
    {
        private UsersController _usersController;

        [Fact(DisplayName = "É Possível Realizar o realizar BadRequest Create.")]
        public async Task E_Possivel_Invocar_a_Controller_Create_BadRequest(){

            var mock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            mock.Setup(m => m.Post(It.IsAny<UserDtoCreate>())).ReturnsAsync(
                new UserDtoCreateResult{
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    CreateAt = DateTime.UtcNow
                });
            
            _usersController = new UsersController(mock.Object);
            _usersController.ModelState.AddModelError("Name", "É um campo Obrigatório.");

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(),It.IsAny<object>())).Returns("http://localhost:5000");
            _usersController.Url = url.Object;

            var userDtoCreate = new UserDtoCreate{
                Name = nome,
                Email = email
            };

            var result = await _usersController.Post(userDtoCreate);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}