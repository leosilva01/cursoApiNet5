using System;
using System.Collections.Generic;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UserMapper :BaseTestService
    {
        [Fact (DisplayName="É Possível Mapear os Modelos.")]
        public void E_Possivel_Mapear_os_Models(){
            
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var listaEntity = new List<UserEntity>();
            for(int i = 0; i < 5; i++)
            {
                var entity = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                listaEntity.Add(entity);
            }

            //Model => Entity
            var modelToEntity = mapper.Map<UserEntity>(model);
            Assert.Equal(modelToEntity.Id, model.Id);
            Assert.Equal(modelToEntity.Name, model.Name);
            Assert.Equal(modelToEntity.Email, model.Email);
            Assert.Equal(modelToEntity.CreateAt, model.CreateAt);
            Assert.Equal(modelToEntity.UpdateAt, model.UpdateAt);

            //Model => Dto
            var modelToDto = mapper.Map<UserDto>(model);
            Assert.Equal(modelToDto.Id, model.Id);
            Assert.Equal(modelToDto.Name, model.Name);
            Assert.Equal(modelToDto.Email, model.Email);
            Assert.Equal(modelToDto.CreateAt, model.CreateAt);

            var listaEntityToDto = mapper.Map<List<UserDto>>(listaEntity);
            Assert.True(listaEntityToDto.Count == listaEntity.Count);
            for(int i = 0; i < listaEntity.Count; i++)
            {
                Assert.Equal(listaEntity[i].Id, listaEntityToDto[i].Id);
                Assert.Equal(listaEntity[i].Name, listaEntityToDto[i].Name);
                Assert.Equal(listaEntity[i].CreateAt, listaEntityToDto[i].CreateAt);
            }

            var modelToCreateDto = mapper.Map<UserDtoCreate>(model);
            Assert.Equal(modelToCreateDto.Name, model.Name);
            Assert.Equal(modelToCreateDto.Email, model.Email);

            var modelToUpdateDto = mapper.Map<UserDtoUpdate>(model);
            Assert.Equal(modelToUpdateDto.Id, model.Id);
            Assert.Equal(modelToUpdateDto.Name, model.Name);
            Assert.Equal(modelToUpdateDto.Email, model.Email);

            //Entity => Dto
            var entityToDto = mapper.Map<UserDto>(modelToEntity);
            Assert.Equal(entityToDto.Id, modelToEntity.Id);
            Assert.Equal(entityToDto.Name, modelToEntity.Name);
            Assert.Equal(entityToDto.Email, modelToEntity.Email);
            Assert.Equal(entityToDto.CreateAt, modelToEntity.CreateAt);

            var entityToCreateDtoResult = mapper.Map<UserDtoCreateResult>(modelToEntity);
            Assert.Equal(entityToCreateDtoResult.Id, modelToEntity.Id);
            Assert.Equal(entityToCreateDtoResult.Name, modelToEntity.Name);
            Assert.Equal(entityToCreateDtoResult.Email, modelToEntity.Email);
            Assert.Equal(entityToCreateDtoResult.CreateAt, modelToEntity.CreateAt);

            var entityToUpdateResult = mapper.Map<UserDtoUpdateResult>(modelToEntity);
            Assert.Equal(entityToUpdateResult.Id, modelToEntity.Id);
            Assert.Equal(entityToUpdateResult.Name, modelToEntity.Name);
            Assert.Equal(entityToUpdateResult.Email, modelToEntity.Email);
            Assert.Equal(entityToUpdateResult.UpdateAt, modelToEntity.UpdateAt);
        }
    }
}