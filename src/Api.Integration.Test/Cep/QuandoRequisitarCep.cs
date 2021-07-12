using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Cep
{
    public class QuandoRequisitarCep : BaseIntegration
    {
        
        
        [Fact(DisplayName = "É Possível realizar CRUD do Cep.")]
        public async Task E_Possivel_Realizar_Crud_Cep(){
            
            await AdicionarToken();

            var municipioDto = new MunicipioDtoCreate{
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = new Guid("bd08208b-bfca-47a4-9cd0-37e4e1fa5006")
            };

            //Adicionando um municipio para teste.
            response = await PostJsonAsync(municipioDto, $"{hostApi}/Municipios", client);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(jsonResult);
           
            Assert.NotNull(registroPost);
            Assert.NotNull(registroPost.Id);
            Assert.NotNull(registroPost.CreateAt);
            Assert.Equal(registroPost.Nome, municipioDto.Nome);
            Assert.Equal(registroPost.CodIBGE, municipioDto.CodIBGE);
            Assert.Equal(registroPost.UfId, municipioDto.UfId);

            var cepDto = new CepDtoCreate{
                Cep = Faker.Address.ZipCode(),
                Logradouro = Faker.Address.StreetAddress(),
                MunicipioId = registroPost.Id,
                Numero = "",
            };

            //Post
            response = await PostJsonAsync(cepDto, $"{hostApi}/Ceps", client);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var cepCriado = JsonConvert.DeserializeObject<CepDtoCreateResult>(jsonResult);
           
            Assert.NotNull(cepCriado);
            Assert.NotNull(cepCriado.Id);
            Assert.NotNull(cepCriado.CreateAt);
            Assert.Equal(cepCriado.Cep, cepDto.Cep);
            Assert.Equal(cepCriado.Numero, "S/N");
            Assert.Equal(cepCriado.Logradouro, cepDto.Logradouro);
            Assert.Equal(cepCriado.MunicipioId, cepDto.MunicipioId);

            //Put
            var cepDtoAlterado = new CepDtoUpdate{
                Id = cepCriado.Id,
                Cep = Faker.Address.ZipCode(),
                Logradouro = Faker.Address.StreetAddress(),
                MunicipioId = registroPost.Id,
                Numero = "11"
            };
            response = await client.PutAsync($"{hostApi}/Ceps", 
                                             new StringContent(JsonConvert.SerializeObject(cepDtoAlterado),
                                                 System.Text.Encoding.UTF8, "application/json"));
                
            jsonResult = await response.Content.ReadAsStringAsync();
            var cepAlterado = JsonConvert.DeserializeObject<CepDtoUpdateResult>(jsonResult);
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(cepAlterado);
            Assert.NotNull(cepAlterado.UpdateAt);
            Assert.Equal(cepAlterado.Id, cepDtoAlterado.Id);
            Assert.Equal(cepAlterado.Cep, cepDtoAlterado.Cep);
            Assert.Equal(cepAlterado.Logradouro, cepDtoAlterado.Logradouro);
            Assert.Equal(cepAlterado.MunicipioId, cepDtoAlterado.MunicipioId);
            Assert.Equal(cepAlterado.Numero, cepDtoAlterado.Numero);

            //Get
            response = await client.GetAsync($"{hostApi}/Ceps/{cepAlterado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<CepDto>(jsonResult);
            Assert.NotNull(registroSelecionado);

            //Get by Cep
            response = await client.GetAsync($"{hostApi}/Ceps/byCep/{cepAlterado.Cep}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            registroSelecionado = JsonConvert.DeserializeObject<CepDto>(jsonResult);
            
            Assert.NotNull(registroSelecionado);

            //Delete
            response = await client.DeleteAsync($"{hostApi}/Ceps/{cepAlterado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await client.DeleteAsync($"{hostApi}/Municipios/{cepAlterado.MunicipioId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //Get depois do Delete
            response = await client.GetAsync($"{hostApi}/Ceps/{cepAlterado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            response = await client.GetAsync($"{hostApi}/Municipios/{cepAlterado.MunicipioId}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}