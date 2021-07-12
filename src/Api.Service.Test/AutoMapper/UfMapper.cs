using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Uf;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UfMapper : BaseTestService
    {

        [Fact(DisplayName = "É Possível Mapear os Modelos de UF.")]
        public void E_Possivel_Mapear_os_Modelos_Uf(){
            
            var model = new UfModel{
                Id = Guid.NewGuid(),
                Nome = Faker.Address.UsState(),
                Sigla = Faker.Address.UsState().Substring(1,3),
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            var entityList = new List<UfEntity>();

            for(int i = 0; i < 5; i++){

                var item = new UfEntity{
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsStateAbbr(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                entityList.Add(item);
            }


            //Model => Entity
            var entity = mapper.Map<UfEntity>(model);
            Assert.NotNull(entity);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.Sigla, model.Sigla);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            //Entity => Dto
            var ufDto = mapper.Map<UfDto>(entity);
            Assert.NotNull(ufDto);
            Assert.Equal(entity.Id, ufDto.Id);
            Assert.Equal(entity.Nome, ufDto.Nome);
            Assert.Equal(entity.Sigla, ufDto.Sigla);

            var listaDto = mapper.Map<List<UfDto>>(entityList);
            Assert.True(listaDto.Count() == entityList.Count());

            for(int i = 0; i < listaDto.Count(); i++){
                Assert.Equal(listaDto[i].Id, entityList[i].Id);
                Assert.Equal(listaDto[i].Nome, entityList[i].Nome);
                Assert.Equal(listaDto[i].Sigla, entityList[i].Sigla);
            }

            //Model => Dto 
            var ufDtoModel = mapper.Map<UfDto>(model);
            Assert.NotNull(ufDtoModel);
            Assert.Equal(model.Id, ufDtoModel.Id);
            Assert.Equal(model.Nome, ufDtoModel.Nome);
            Assert.Equal(model.Sigla, ufDtoModel.Sigla);
            

        }
    }
}