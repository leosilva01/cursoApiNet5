using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Municipio
{
    public class QuandoRequisitarMunicipio : BaseIntegration
    {
        
        private string _nome;
        private int _codIBGE;
        private string _nomeAlterado;
        private int _codIBGEAlterado;
        private Guid _ufId;
        
        [Fact(DisplayName = "É Possível realizar CRUD do Município.")]
        public async Task E_Possivel_Realizar_Crud_Municipio(){
            
            await AdicionarToken();

            _nome = Faker.Address.City();
            _codIBGE = Faker.RandomNumber.Next(1000000, 9999999);
            _ufId = new Guid("bd08208b-bfca-47a4-9cd0-37e4e1fa5006");
            _nomeAlterado = Faker.Address.City();
            _codIBGEAlterado = Faker.RandomNumber.Next(1000000, 9999999);

            var municipioDtoCreate = new MunicipioDtoCreate{
                Nome = _nome,
                CodIBGE = _codIBGE,
                UfId = _ufId
            };

            var municipioDtoUpdate = new MunicipioDtoUpdate{
                Nome = _nomeAlterado,
                CodIBGE = _codIBGEAlterado,
                UfId = _ufId
            };

            //Post
            response = await PostJsonAsync(municipioDtoCreate, $"{hostApi}/Municipios", client);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var registroCriado = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(jsonResult);
           
            Assert.NotNull(registroCriado);
            Assert.NotNull(registroCriado.Id);
            Assert.NotNull(registroCriado.CreateAt);
            Assert.Equal(registroCriado.Nome, _nome);
            Assert.Equal(registroCriado.CodIBGE, _codIBGE);
            Assert.Equal(registroCriado.UfId, _ufId);

            //GetAll
            response = await client.GetAsync($"{hostApi}/Municipios");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<MunicipioDto>>(jsonResult);
            
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() > 0);
            Assert.True(listaFromJson.Where(m => m.Id == registroCriado.Id).Count() == 1);

            //Get
            response = await client.GetAsync($"{hostApi}/Municipios/{registroCriado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<MunicipioDto>(jsonResult);
           
            Assert.NotNull(registroSelecionado);
            Assert.NotNull(registroSelecionado.Id);
            Assert.Equal(registroSelecionado.Nome, registroCriado.Nome);
            Assert.Equal(registroSelecionado.CodIBGE, registroCriado.CodIBGE);
            Assert.Equal(registroSelecionado.UfId, registroCriado.UfId);

            //Put
            municipioDtoUpdate.Id = registroSelecionado.Id;
            response = await client.PutAsync($"{hostApi}/Municipios", 
                                             new StringContent(JsonConvert.SerializeObject(municipioDtoUpdate),
                                                 System.Text.Encoding.UTF8, "application/json"));
                
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAlterado = JsonConvert.DeserializeObject<MunicipioDtoUpdateResult>(jsonResult);
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(registroAlterado);
            Assert.NotNull(registroAlterado.Id);
            Assert.NotNull(registroAlterado.UpdateAt);
            Assert.Equal(registroAlterado.Nome, _nomeAlterado);
            Assert.Equal(registroAlterado.CodIBGE, _codIBGEAlterado);
            Assert.Equal(registroAlterado.UfId, _ufId);

            //GetById
            response = await client.GetAsync($"{hostApi}/Municipios/Complete/{registroAlterado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroCompletoSelecionado = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            
            Assert.NotNull(registroCompletoSelecionado);
            Assert.NotNull(registroCompletoSelecionado.Id);
            Assert.NotNull(registroCompletoSelecionado.Uf);
            Assert.Equal(registroCompletoSelecionado.Nome, registroAlterado.Nome);
            Assert.Equal(registroCompletoSelecionado.CodIBGE, registroAlterado.CodIBGE);
            Assert.Equal(registroCompletoSelecionado.UfId, registroAlterado.UfId);
            Assert.Equal(registroCompletoSelecionado.Uf.Id, registroAlterado.UfId);
            Assert.Equal(registroCompletoSelecionado.Uf.Sigla, "DF");

            //GetByIBGE
            response = await client.GetAsync($"{hostApi}/Municipios/CompleteByIBGE/{registroAlterado.CodIBGE}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            registroCompletoSelecionado = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            
            Assert.NotNull(registroCompletoSelecionado);
            Assert.NotNull(registroCompletoSelecionado.Id);
            Assert.NotNull(registroCompletoSelecionado.Uf);
            Assert.Equal(registroCompletoSelecionado.Nome, registroAlterado.Nome);
            Assert.Equal(registroCompletoSelecionado.CodIBGE, registroAlterado.CodIBGE);
            Assert.Equal(registroCompletoSelecionado.UfId, registroAlterado.UfId);
            Assert.Equal(registroCompletoSelecionado.Uf.Id, registroAlterado.UfId);
            Assert.Equal(registroCompletoSelecionado.Uf.Sigla, "DF");

            //Delete
            response = await client.DeleteAsync($"{hostApi}/Municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //Get depois do delete
            response = await client.GetAsync($"{hostApi}/Municipios/{registroCriado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}