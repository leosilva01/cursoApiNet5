using System;
using System.Collections.Generic;
using Api.Domain.Dtos.User;

namespace Api.Service.Test.User
{
    public class UserTests
    {
        public static string nameUser { get; set; }
        public static string emailUser { get; set; }
        public static string nomeAlterado { get; set; }
        public static string emailAlterado { get; set; }
        public static Guid idUser { get; set; }
        public UserDto userDto;
        public List<UserDto> userDtoList = new List<UserDto>();
        public UserDtoCreate userDtoCreate;
        public UserDtoCreateResult userDtoCreateResult;
        public UserDtoUpdate userDtoUpdate;
        public UserDtoUpdateResult userDtoUpdateResult;

        public UserTests()
        {
            idUser = Guid.NewGuid();
            nameUser = Faker.Name.FullName();
            emailUser = Faker.Internet.Email();
            nomeAlterado = Faker.Name.FullName();
            emailAlterado = Faker.Internet.Email();

            for(int i = 0; i < 10; i++)
            {
                var dto = new UserDto()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };
                userDtoList.Add(dto);
            }

            userDto = new UserDto(){
                Id = idUser,
                Name = nameUser,
                Email = emailUser,
                CreateAt = DateTime.UtcNow
            };

            userDtoCreate = new UserDtoCreate(){
                Name = nameUser,
                Email = emailUser,

            };

            userDtoCreateResult = new UserDtoCreateResult(){
                Id = idUser,
                Name = nameUser,
                Email = emailUser,
                CreateAt = DateTime.UtcNow
            };

            userDtoUpdate = new UserDtoUpdate(){
                Id = idUser,
                Name = nomeAlterado,
                Email = emailAlterado,

            };

            userDtoUpdateResult = new UserDtoUpdateResult(){
                Id = idUser,
                Name = nomeAlterado,
                Email = emailAlterado,
                UpdateAt = DateTime.UtcNow
            };
        }
    }
}