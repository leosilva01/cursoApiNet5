using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Uf
{
    public class QuandoRequisitarUf : BaseIntegration
    {
        
        [Fact(DisplayName = "É Possível realizar métodos da UF.")]
        public async Task E_Possivel_Realizar_Metodos_Uf(){
            
            await AdicionarToken();

            //GetAll
            response = await client.GetAsync($"{hostApi}/ufs");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<UfDto>>(jsonResult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() == 27);
            Assert.True(listaFromJson.Where(u => u.Sigla == "DF").Count() == 1);

            //Get
            var id = listaFromJson.Where(u => u.Sigla == "DF").SingleOrDefault().Id;
            response = await client.GetAsync($"{hostApi}/ufs/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            var resultFromJson = JsonConvert.DeserializeObject<UfDto>(result);
            Assert.NotNull(resultFromJson);
            Assert.True(resultFromJson.Sigla == "DF");
            Assert.True(resultFromJson.Nome == "Distrito Federal");
        }
    }
}