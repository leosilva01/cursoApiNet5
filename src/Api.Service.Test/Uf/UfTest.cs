using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.Uf
{
    public class UfTest
    {
        public static string Nome { get; set; }
        public static string Sigla { get; set; }
        public static Guid IdUf { get; set; }

        public List<UfDto> ufDtoList = new List<UfDto>();

        public UfDto ufDto;

        public UfTest()
        {

            IdUf = Guid.NewGuid();
            Nome = Faker.Address.UsState();
            Sigla = Faker.Address.UsStateAbbr();

            for(int i = 0; i < 10; i++){
                
                var item = new UfDto{
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsStateAbbr()
                };
                ufDtoList.Add(item);
            }

            ufDto = new UfDto{
                
                Id = Guid.NewGuid(),
                Nome = Faker.Address.UsState(),
                Sigla = Faker.Address.UsStateAbbr()
            };

        }
    }
}