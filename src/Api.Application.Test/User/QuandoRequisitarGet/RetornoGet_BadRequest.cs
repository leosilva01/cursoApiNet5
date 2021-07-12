using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.QuandoRequisitarGet
{
    public class RetornoGet_BadRequest
    {
        private UsersController _usersController;

        [Fact(DisplayName = "É Possível Realizar BadRequest no Método Get.")]
        public async Task E_Possivel_Invocar_a_Controller_Get_BadRequest(){

            var mock = new Mock<IUserService>();
            var id = Guid.NewGuid();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            mock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(new UserDto{
                Id = id,
                Name = nome,
                Email = email,
                CreateAt = DateTime.UtcNow
            });

            _usersController = new UsersController(mock.Object);
            _usersController.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _usersController.Get(id);
            Assert.True(result is BadRequestObjectResult);
        }
    }
}