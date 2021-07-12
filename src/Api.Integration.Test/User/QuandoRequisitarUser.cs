using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.User
{
    public class QuandoRequisitarUser : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }


        [Fact]
        public async Task E_Possivel_Realizar_CRUD_User(){
            
            await AdicionarToken();

            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserDtoCreate(){
                Email = _email,
                Name = _name
            };


            //POST
            var response = await PostJsonAsync(userDto, $"{hostApi}/users", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<UserDtoCreateResult>(postResult);
           
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, registroPost.Name);
            Assert.Equal(_email, registroPost.Email);
            Assert.True(registroPost.Id != default(Guid));


            //GET
            response = await client.GetAsync($"{hostApi}/users");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonResult);
            Assert.True(listaFromJson.Count() > 0);
            Assert.True(listaFromJson.Where(u => u.Id.Equals(registroPost.Id)).Count() == 1);
            
            
            //PUT
            var updateUserDto = new UserDtoUpdate{
                Id = registroPost.Id,
                Name = Faker.Name.First(),
                Email = Faker.Internet.Email()
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateUserDto), Encoding.UTF8, "application/json");
            
            response = await client.PutAsync($"{hostApi}/users", stringContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();

            var registroAtualizado = JsonConvert.DeserializeObject<UserDtoUpdateResult>(jsonResult);

            Assert.NotEqual(registroPost.Name, registroAtualizado.Name);
            Assert.NotEqual(registroPost.Email, registroAtualizado.Email);

            //GET id
            response = await client.GetAsync($"{hostApi}/users/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<UserDto>(jsonResult);
            Assert.NotNull(registroSelecionado.Id);
            Assert.Equal(registroSelecionado.Id, registroAtualizado.Id);
            Assert.Equal(registroSelecionado.Name, registroAtualizado.Name);
            Assert.Equal(registroSelecionado.Email, registroAtualizado.Email);

            //DELETE
            response = await client.DeleteAsync($"{hostApi}/users/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            response = await client.GetAsync($"{hostApi}/users/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}