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
    public class RetornoGet
    {
        private UsersController _usersController;

        [Fact(DisplayName = "É Possível Realizar o Método Get.")]
        public async Task E_Possivel_Invocar_a_Controller_Get(){

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

            var result = await _usersController.Get(id);
            Assert.True(result is OkObjectResult);
            var resultValue = ((OkObjectResult)result).Value as UserDto;
            resultValue.Id = id;
            resultValue.Name.Equals(nome);
            resultValue.Email.Equals(email);
        }
    }
}