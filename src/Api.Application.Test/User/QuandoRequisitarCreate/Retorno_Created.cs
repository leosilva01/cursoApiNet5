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
    public class Retorno_Created
    {
        private UsersController _usersController;

        [Fact(DisplayName = "É Possível Realizar o Created.")]
        public async Task E_Possivel_Invocar_a_Controller_Create(){

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

            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(),It.IsAny<object>())).Returns("http://localhost:5000");
            _usersController.Url = url.Object;

            var userDtoCreate = new UserDtoCreate{
                Name = nome,
                Email = email
            };

            var result = await _usersController.Post(userDtoCreate);
            Assert.True(result is CreatedResult);
            
            //Objeto de retorno da controller
            var resultValue = ((CreatedResult)result).Value as UserDtoCreateResult;
            Assert.NotNull(resultValue);
            Assert.Equal(resultValue.Name, userDtoCreate.Name);
            Assert.Equal(resultValue.Email, userDtoCreate.Email);


        }
    }
}