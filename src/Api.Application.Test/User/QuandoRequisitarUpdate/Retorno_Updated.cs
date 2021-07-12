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
    public class Retorno_Updated
    {
        private UsersController _usersController;

        [Fact(DisplayName = "É Possível Realizar o Updated.")]
        public async Task E_Possivel_Invocar_a_Controller_Update(){

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

            var userDtoUpdate = new UserDtoUpdate{
                Id = id,
                Name = nome,
                Email = email
            };

            var result = await _usersController.Put(userDtoUpdate);
            Assert.True(result is OkObjectResult);

            UserDtoUpdateResult resultValue = ((OkObjectResult)result).Value as UserDtoUpdateResult;
            Assert.NotNull(resultValue);
            Assert.Equal(resultValue.Id, userDtoUpdate.Id);
            Assert.Equal(resultValue.Name, userDtoUpdate.Name);
            Assert.Equal(resultValue.Email, userDtoUpdate.Email);

        }
    }
}