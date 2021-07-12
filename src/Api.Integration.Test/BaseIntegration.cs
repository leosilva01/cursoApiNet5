using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Api.CrossCutting.Mappings;
using Api.Data.Context;
using Api.Domain.Dtos;
using application;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Api.Integration.Test
{
    public abstract class BaseIntegration : IDisposable
    {
        public MyContext myContext {get; private set;}
        public HttpClient client{get; private set;}
        public IMapper mapper {get; set;}
        public string hostApi {get; set;}
        public HttpResponseMessage response {get; set;}
        
        public BaseIntegration()
        {
            hostApi = "http://localhost:5000/api";
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            
            myContext = server.Host.Services.GetService((typeof(MyContext))) as MyContext;
            
            //Vai criar o server de integração com os dados que estão na migrate.
            myContext.Database.Migrate();

            mapper = new AutoMapperFixture().GetMapper();

            client = server.CreateClient();

        }

        public async Task AdicionarToken(){

            var loginDto = new LoginDto()
            {
                Email = "admin@admin.com"
            };
            
            var result = await PostJsonAsync(loginDto, $"{hostApi}/login", client);
            var jsonLogin = await result.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResposeDto>(jsonLogin);

            client.DefaultRequestHeaders.Authorization  = new AuthenticationHeaderValue("Bearer", loginObject.accessToken);
        }

        //Método para fazer o envio da requisição POST.
        public static async Task<HttpResponseMessage> PostJsonAsync(object dataclass, string url, HttpClient client){
            var result = await client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(dataclass),
                System.Text.Encoding.UTF8, "application/json"));
            return result;
        }

        public void Dispose()
        {
            myContext.Dispose();
            client.Dispose();
        }
    }


    public class AutoMapperFixture : IDisposable
    {
        public IMapper GetMapper(){
            
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new ModelToEntityProfile());
                    cfg.AddProfile(new DtoToModelProfile());
                    cfg.AddProfile(new EntityToDtoProfile());
                });

            return config.CreateMapper();
        }

        public void Dispose()
        {
        }
    }
}