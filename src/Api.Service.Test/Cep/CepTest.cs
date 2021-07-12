using System;
using System.Collections.Generic;
using Api.Domain.Dtos.Cep;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Dtos.Uf;

namespace Api.Service.Test.Cep
{
    public class CepTest
    {
        public Guid Id;
        public string Cep;
        public string CepAlterado;
        public string Logradouro;
        public string LogradouroAlterado;
        public string Numero;
        public string NumeroAlterado;
        public Guid MunicipioId;
        public CepDto dto;
        public CepDtoCreate dtoCreate;
        public CepDtoUpdate dtoUpdate;
        public CepDtoCreateResult dtoCreateResult;
        public CepDtoUpdateResult dtoUpdateResult;
        public List<CepDto> listDto = new List<CepDto>();

        public CepTest()
        {
            Id = Guid.NewGuid();
            Cep = Faker.Address.ZipCode();
            Logradouro = Faker.Address.StreetAddress();
            Numero = "Número";
            MunicipioId = Guid.NewGuid();
            NumeroAlterado = "Alterado";
            LogradouroAlterado = Faker.Address.StreetAddress();
            CepAlterado = Faker.Address.ZipCode();

            dto = new CepDto{
                Id = Id,
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = MunicipioId,
                Municipio = new MunicipioDtoCompleto{
                    Id = MunicipioId,
                    CodIBGE = Faker.RandomNumber.Next(1000000,9999999),
                    Nome = Faker.Address.City(),
                    UfId = Guid.NewGuid(),
                    Uf = new UfDto{
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsStateAbbr(),
                    }
                }
            };

            for(int i = 0; i < 5; i++){
                
                var item = new CepDto{
                    Id = Guid.NewGuid(),
                    Cep = Faker.Address.ZipCode(),
                    Logradouro = Faker.Address.StreetAddress(),
                    Numero = "Numero_" + i,
                    MunicipioId = Guid.NewGuid(),
                    Municipio = new MunicipioDtoCompleto{
                        Id = Guid.NewGuid(),
                        CodIBGE = Faker.RandomNumber.Next(1000000,9999999),
                        Nome = Faker.Address.City(),
                        UfId = Guid.NewGuid(),
                        Uf = new UfDto{
                            Id = Guid.NewGuid(),
                            Nome = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsStateAbbr(),
                        }
                    }
                };
            }

            dtoCreate = new CepDtoCreate{
                Cep = Cep,
                Logradouro = Logradouro,
                MunicipioId = MunicipioId,
                Numero = Numero
            };

            dtoCreateResult = new CepDtoCreateResult{
                Cep = Cep,
                CreateAt = DateTime.Now,
                Id = Id,
                Logradouro = Logradouro,
                MunicipioId = MunicipioId,
                Numero = Numero
            };

            dtoUpdate = new CepDtoUpdate{
                Cep = CepAlterado,
                Id = Id,
                Logradouro = LogradouroAlterado,
                MunicipioId = MunicipioId,
                Numero = NumeroAlterado
            };

            dtoUpdateResult = new CepDtoUpdateResult{
                Cep = CepAlterado,
                Id = Id,
                Logradouro = LogradouroAlterado,
                MunicipioId = MunicipioId,
                Numero = NumeroAlterado,
                UpdateAt = DateTime.Now
            };
        }
    }
}