using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.QuandoRequisitarDelete
{
    public class Retorno_Delete
    {
        private UsersController _usersController;

        [Fact(DisplayName = "É Possível Realizar o Deleted.")]
        public async Task E_Possivel_Invocar_a_Controller_Delete(){

            var mock = new Mock<IUserService>();
            var id = Guid.NewGuid();

            mock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _usersController = new UsersController(mock.Object);

            var result = await _usersController.Delete(id);
            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value;
            Assert.NotNull(resultValue);
            Assert.True((Boolean)resultValue);
        }
    }
}