using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Cep;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class CepMapper : BaseTestService
    {
        [Fact(DisplayName = "É Possível Mapear os Modelos de CEP.")]
        public void E_Possivel_Mapear_os_Modelos_Cep(){

            var model = new CepModel{
                Id = Guid.NewGuid(),
                Cep = Faker.Address.ZipCode(),
                Logradouro = Faker.Address.StreetAddress(),
                Numero = "",
                MunicipioId = Guid.NewGuid(),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            var entityList = new List<CepEntity>();

            for(int i = 0; i < 5; i++){

                var item = new CepEntity{
                    Id = Guid.NewGuid(),
                    Cep = Faker.Address.ZipCode(),
                    Logradouro = Faker.Address.StreetAddress(),
                    Numero = "",
                    MunicipioId = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Municipio = new MunicipioEntity{
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.City(),
                        CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        UfId = Guid.NewGuid(),
                        Uf = new UfEntity{
                            Id = Guid.NewGuid(),
                            Nome = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsStateAbbr(),
                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now
                        }
                    }
                };

                entityList.Add(item);
            }

            //Model => Entity
            var entity = mapper.Map<CepEntity>(model);
            Assert.NotNull(entity);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Cep, entity.Cep);
            Assert.Equal(model.Logradouro, entity.Logradouro);
            Assert.Equal(model.Numero, entity.Numero);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.MunicipioId, entity.MunicipioId);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);

            //Entity => Dto
            var cepDto = mapper.Map<CepDto>(entity);
            Assert.NotNull(cepDto);
            Assert.Equal(cepDto.Id, entity.Id);
            Assert.Equal(cepDto.Cep, entity.Cep);
            Assert.Equal(cepDto.Logradouro, entity.Logradouro);
            Assert.Equal(cepDto.Numero, entity.Numero);
            Assert.Equal(cepDto.MunicipioId, entity.MunicipioId);

            //Entity => Dto
            var cepDtoCompleto = mapper.Map<CepDto>(entityList.FirstOrDefault());
            Assert.NotNull(cepDtoCompleto);
            Assert.Equal(cepDtoCompleto.Id, entityList.FirstOrDefault().Id);
            Assert.Equal(cepDtoCompleto.Cep, entityList.FirstOrDefault().Cep);
            Assert.Equal(cepDtoCompleto.Logradouro, entityList.FirstOrDefault().Logradouro);
            Assert.Equal(cepDtoCompleto.Numero, entityList.FirstOrDefault().Numero);
            Assert.Equal(cepDtoCompleto.MunicipioId, entityList.FirstOrDefault().MunicipioId);
            Assert.NotNull(cepDtoCompleto.Municipio);
            Assert.NotNull(cepDtoCompleto.Municipio.Uf);


            var listaDto = mapper.Map<List<CepDto>>(entityList);
            Assert.NotNull(listaDto);
            Assert.True(listaDto.Count() == entityList.Count());
            
            for(int i = 0; i < listaDto.Count(); i++){

                Assert.Equal(listaDto[i].Id, entityList[i].Id);
                Assert.Equal(listaDto[i].Cep, entityList[i].Cep);
                Assert.Equal(listaDto[i].Logradouro, entityList[i].Logradouro);
                Assert.Equal(listaDto[i].MunicipioId, entityList[i].MunicipioId);
                Assert.Equal(listaDto[i].Numero, entityList[i].Numero);
                Assert.Equal(listaDto[i].Municipio.Nome, entityList[i].Municipio.Nome);
            }

            var cepDtoCreateResult = mapper.Map<CepDtoCreateResult>(entity);
            Assert.NotNull(cepDtoCreateResult);
            Assert.Equal(cepDtoCreateResult.Id, entity.Id);
            Assert.Equal(cepDtoCreateResult.Cep, entity.Cep);
            Assert.Equal(cepDtoCreateResult.Logradouro, entity.Logradouro);
            Assert.Equal(cepDtoCreateResult.Numero, entity.Numero);
            Assert.Equal(cepDtoCreateResult.MunicipioId, entity.MunicipioId);
            Assert.Equal(cepDtoCreateResult.CreateAt, entity.CreateAt);

            var cepDtoUpdateResult = mapper.Map<CepDtoUpdateResult>(entity);
            Assert.NotNull(cepDtoUpdateResult);
            Assert.Equal(cepDtoUpdateResult.Id, entity.Id);
            Assert.Equal(cepDtoUpdateResult.Cep, entity.Cep);
            Assert.Equal(cepDtoUpdateResult.Logradouro, entity.Logradouro);
            Assert.Equal(cepDtoUpdateResult.Numero, entity.Numero);
            Assert.Equal(cepDtoUpdateResult.MunicipioId, entity.MunicipioId);
            Assert.Equal(cepDtoUpdateResult.UpdateAt, entity.UpdateAt);

            //Dto => Model
            cepDto.Numero = "";
            var cepModel = mapper.Map<CepModel>(cepDto);
            Assert.NotNull(cepDto);
            Assert.Equal(cepDto.Id, cepModel.Id);
            Assert.Equal(cepDto.Cep, cepModel.Cep);
            Assert.Equal(cepDto.Logradouro, cepModel.Logradouro);
            Assert.Equal("S/N", cepModel.Numero);
            Assert.Equal(cepDto.MunicipioId, cepModel.MunicipioId);

            var cepDtoCreate = mapper.Map<CepDtoCreate>(cepModel);
            Assert.NotNull(cepDtoCreate);
            Assert.Equal(cepDtoCreate.Cep, cepModel.Cep);
            Assert.Equal(cepDtoCreate.Logradouro, cepModel.Logradouro);
            Assert.Equal(cepDtoCreate.Numero, cepModel.Numero);
            Assert.Equal(cepDtoCreate.MunicipioId, cepModel.MunicipioId);

            var cepDtoUpdate = mapper.Map<CepDtoUpdate>(cepModel);
            Assert.NotNull(cepDtoUpdate);
            Assert.Equal(cepDtoUpdate.Id, cepModel.Id);
            Assert.Equal(cepDtoUpdate.Cep, cepModel.Cep);
            Assert.Equal(cepDtoUpdate.Logradouro, cepModel.Logradouro);
            Assert.Equal(cepDtoUpdate.Numero, cepModel.Numero);
            Assert.Equal(cepDtoUpdate.MunicipioId, cepModel.MunicipioId);


        }
    }
}