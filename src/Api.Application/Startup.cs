using System;
using System.Collections.Generic;
using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace application
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        IWebHostEnvironment _environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Faz as configurações para os testes de integração.
            if(_environment.IsEnvironment("Testing"))
            {
                Environment.SetEnvironmentVariable("DB_CONNECTION", "Persist Security Info =True;Server=localhost;Port=3306;Database=dbAPI_Integration;Uid=root;Pwd=mudar@123");
                Environment.SetEnvironmentVariable("DATABASE", "MYSQL");
                Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
                Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
                Environment.SetEnvironmentVariable("Issuer", "ExemploIssuer");
                Environment.SetEnvironmentVariable("Seconds", "28800");
            }


            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);

//          Adiciona os profiles para o autoMapper saber fazer as transformações.
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

//          Configurando injeção de dependência e obrigatoriedade de autenticação para a aplicação. é necessário incluir as annotations nos metodos necessários.
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(authOptions=>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience =  Environment.GetEnvironmentVariable("Audience");
                paramsValidation.ValidIssuer =  Environment.GetEnvironmentVariable("Issuer");
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            });
//***************** FIM CONFIGURAÇÃO DE AUTENTICAÇÃO **********************************

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                    { 
                        Title = "Curso AspNetCore 5.0", 
                        Version = "v1",
                        Description = "Arquitetura DDD",
                        TermsOfService = new Uri("http://www.google.com"),
                        Contact = new OpenApiContact
                        {
                            Name = "Léo Silva",
                            Email = "leo.leomota@gmail.com",
                            Url = new Uri("http://www.google.com")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Termo de Licença de Uso",
                            Url = new Uri("http://www.google.com")
                        }
                    
                    });

                    //para incluir no Swagger botão para inclusão e Token 
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Entre com o Token JWT",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                    
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme {
                                Reference = new OpenApiReference {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            }, new List<string>()
                        }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso AspNetCore 5.0");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
