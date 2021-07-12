using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;
using Api.Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CepsController : ControllerBase
    {
        private ICepService _service { get; set; }

        public CepsController(ICepService service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}", Name = "GetCepWithId")]
        public async Task<ActionResult> Get(Guid id) {

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            try {
               
                var result = await _service.Get(id);

                if(result == null){
                    return NotFound();
                }

                return Ok(result);

            } catch(ArgumentException e) {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("byCep/{cep}", Name = "GetCepWithCep")]
        public async Task<ActionResult> Get(string cep) {

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            try {
               
                var result = await _service.Get(cep);

                if(result == null){
                    return NotFound();
                }

                return Ok(result);

            } catch(ArgumentException e) {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CepDtoCreate cepDtoCreate) {

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            try {
               
                var result = await _service.Post(cepDtoCreate);

                if(result == null){
                    return BadRequest();
                }

                return Created(new Uri(Url.Link("GetCepWithId", new {id = result.Id} )), result);

            } catch(ArgumentException e) {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CepDtoUpdate cepDtoUpdate) {

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            try {
               
                var result = await _service.Put(cepDtoUpdate);

                if(result == null){
                    return NotFound();
                }

                return Ok(result);

            } catch(ArgumentException e) {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) {

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            try {
               
                var result = await _service.Delete(id);

                return Ok(result);

            } catch(ArgumentException e) {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);

            }
        }
    }
}