using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.Municipio
{
    public class MunicipioTest
    {
        public static Guid Id;
        public static string Nome;
        public static int CodIBGE;
        public static int CodIBGEAlterado;
        public static string NomeAlterado;
        public static Guid UfId;
        public MunicipioDto dto;

        public MunicipioDtoCompleto dtoCompleto;
        public MunicipioDtoCreate dtoCreate;
        public MunicipioDtoUpdate dtoUpdate;
        public MunicipioDtoCreateResult dtoCreateResult;
        public MunicipioDtoUpdateResult dtoUpdateResult;


        public List<MunicipioDto> dtoList = new List<MunicipioDto>();

        public MunicipioTest()
        {
            Id = Guid.NewGuid();
            Nome = Faker.Address.City();
            CodIBGE = Faker.RandomNumber.Next(1000000, 9999999);
            UfId = Guid.NewGuid();
            NomeAlterado = Faker.Address.City();
            CodIBGEAlterado = Faker.RandomNumber.Next(1000000, 9999999);

            for(int i = 0; i < 10; i++){
                
                var item = new MunicipioDto{
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = Guid.NewGuid()
                };
                dtoList.Add(item);
            }

            dto = new MunicipioDto{
                
                Id = Id,
                Nome = Nome,
                CodIBGE = CodIBGE,
                UfId = UfId
            };

            dtoCompleto = new MunicipioDtoCompleto{
                Id = Id,
                Nome = Nome,
                CodIBGE = CodIBGE,
                UfId = UfId,
                Uf = new UfDto{
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsStateAbbr()
                }
                
            };

            dtoCreate = new MunicipioDtoCreate{
                Nome = Nome,
                UfId = UfId,
                CodIBGE = CodIBGE
            };

            dtoCreateResult = new MunicipioDtoCreateResult{
                Id = Id,
                Nome = Nome,
                UfId = UfId,
                CodIBGE = CodIBGE,
                CreateAt = DateTime.Now
            };

            dtoUpdate = new MunicipioDtoUpdate{
                Id = Id,
                Nome = NomeAlterado,
                UfId = UfId,
                CodIBGE = CodIBGEAlterado
            };

            dtoUpdateResult = new MunicipioDtoUpdateResult{
                Id = Id,
                Nome = NomeAlterado,
                UfId = UfId,
                CodIBGE = CodIBGEAlterado,
                UpdateAt = DateTime.Now
            };

        }

    }
}