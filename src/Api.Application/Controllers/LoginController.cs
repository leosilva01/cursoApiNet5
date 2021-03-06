using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto login, [FromServices] ILoginService service){
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            if(login == null){
                return BadRequest();
            }

            try{

                var result = await service.FindByLogin(login);
                
                if(login != null){
                    return Ok(result);
                }

                return NotFound();

            }catch(ArgumentException e){
                return StatusCode((int) HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}