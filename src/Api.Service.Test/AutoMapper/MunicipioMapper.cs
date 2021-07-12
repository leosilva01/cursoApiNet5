using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class MunicipioMapper : BaseTestService
    {
        [Fact(DisplayName = "É Possível Mapear os Modelos de Município.")]
        public void E_Possivel_Mapear_os_Modelos_Municipio(){

            var model = new MunicipioModel{
                Id = Guid.NewGuid(),
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                UfId = Guid.NewGuid(),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            var entityList = new List<MunicipioEntity>();

            for(int i = 0; i < 5; i++){

                var item = new MunicipioEntity{
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Uf = new UfEntity{
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsStateAbbr()
                    }
                };

                entityList.Add(item);
            }

            //Model => Entity
            var entity = mapper.Map<MunicipioEntity>(model);
            Assert.NotNull(entity);
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Nome, entity.Nome);
            Assert.Equal(model.CodIBGE, entity.CodIBGE);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);
            Assert.Equal(model.UfId, entity.UfId);

            //Entity => Dto
            var municipioDto = mapper.Map<MunicipioDto>(entity);
            Assert.NotNull(municipioDto);
            Assert.Equal(municipioDto.Id, entity.Id);
            Assert.Equal(municipioDto.Nome, entity.Nome);
            Assert.Equal(municipioDto.CodIBGE, entity.CodIBGE);
            Assert.Equal(municipioDto.UfId, entity.UfId);

            var municipioDtoCompleto = mapper.Map<MunicipioDtoCompleto>(entityList.FirstOrDefault());
            Assert.NotNull(municipioDtoCompleto);
            Assert.Equal(municipioDtoCompleto.Id, entityList.FirstOrDefault().Id);
            Assert.Equal(municipioDtoCompleto.Nome, entityList.FirstOrDefault().Nome);
            Assert.Equal(municipioDtoCompleto.CodIBGE, entityList.FirstOrDefault().CodIBGE);
            Assert.Equal(municipioDtoCompleto.UfId, entityList.FirstOrDefault().UfId);
            Assert.NotNull(municipioDtoCompleto.Uf);
            Assert.Equal(municipioDtoCompleto.Uf.Id, entityList.FirstOrDefault().Uf.Id);
            Assert.Equal(municipioDtoCompleto.Uf.Nome, entityList.FirstOrDefault().Uf.Nome);
            Assert.Equal(municipioDtoCompleto.Uf.Sigla, entityList.FirstOrDefault().Uf.Sigla);

            var listaDto = mapper.Map<List<MunicipioDto>>(entityList);
            Assert.True(listaDto.Count() == entityList.Count());
            for(int i = 0; i < listaDto.Count(); i++){

                Assert.Equal(listaDto[i].Id, entityList[i].Id);
                Assert.Equal(listaDto[i].Nome, entityList[i].Nome);
                Assert.Equal(listaDto[i].CodIBGE, entityList[i].CodIBGE);
                Assert.Equal(listaDto[i].UfId, entityList[i].UfId);

            }

            var municipioDtoCreateResult = mapper.Map<MunicipioDtoCreateResult>(entity);
            Assert.NotNull(municipioDtoCreateResult);
            Assert.Equal(municipioDtoCreateResult.Id, entity.Id);
            Assert.Equal(municipioDtoCreateResult.Nome, entity.Nome);
            Assert.Equal(municipioDtoCreateResult.CodIBGE, entity.CodIBGE);
            Assert.Equal(municipioDtoCreateResult.CreateAt, entity.CreateAt);
            Assert.Equal(municipioDtoCreateResult.UfId, entity.UfId);

            var municipioDtoUpdateResult = mapper.Map<MunicipioDtoUpdateResult>(entity);
            Assert.NotNull(municipioDtoUpdateResult);
            Assert.Equal(municipioDtoUpdateResult.Id, entity.Id);
            Assert.Equal(municipioDtoUpdateResult.Nome, entity.Nome);
            Assert.Equal(municipioDtoUpdateResult.CodIBGE, entity.CodIBGE);
            Assert.Equal(municipioDtoUpdateResult.UpdateAt, entity.UpdateAt);
            Assert.Equal(municipioDtoUpdateResult.UfId, entity.UfId);

            //Dto => model
            var municipioModel = mapper.Map<MunicipioModel>(municipioDto);
            Assert.NotNull(municipioModel);
            Assert.Equal(municipioModel.Id, municipioDto.Id);
            Assert.Equal(municipioModel.Nome, municipioDto.Nome);
            Assert.Equal(municipioModel.CodIBGE, municipioDto.CodIBGE);
            Assert.Equal(municipioModel.UfId, municipioDto.UfId);

            var municipioDtoCreate = mapper.Map<MunicipioDtoCreate>(municipioModel);
            Assert.NotNull(municipioDtoCreate);
            Assert.Equal(municipioDtoCreate.Nome, municipioModel.Nome);
            Assert.Equal(municipioDtoCreate.CodIBGE, municipioModel.CodIBGE);
            Assert.Equal(municipioDtoCreate.UfId, municipioModel.UfId);

            var municipioDtoUpdate = mapper.Map<MunicipioDtoUpdate>(municipioModel);
            Assert.NotNull(municipioDtoUpdate);
            Assert.Equal(municipioDtoUpdate.Id, municipioModel.Id);
            Assert.Equal(municipioDtoUpdate.Nome, municipioModel.Nome);
            Assert.Equal(municipioDtoUpdate.CodIBGE, municipioModel.CodIBGE);
            Assert.Equal(municipioDtoUpdate.UfId, municipioModel.UfId);

        }
    }
}