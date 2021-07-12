using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.User.QuandoRequisitarDelete
{
    public class Retorno_Delete_BadRequest
    {
        private UsersController _usersController;

        [Fact(DisplayName = "É Possível Realizar o BadRequest Deleted.")]
        public async Task E_Possivel_Invocar_a_Controller_Delete_BadRequest(){

            var mock = new Mock<IUserService>();
            mock.Setup(m => m.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            _usersController = new UsersController(mock.Object);
            _usersController.ModelState.AddModelError("Id", "Formato Inválido.");

            var result = await _usersController.Delete(default(Guid));
            Assert.True(result is BadRequestObjectResult);
            Assert.False(_usersController.ModelState.IsValid);
        }
    }
}