using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class CepCrudCompleto : DbTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;
            
        public CepCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.serviceProvider;
        }

        [Fact(DisplayName = "CRUD de Cep.")]
        [Trait("CRUD", "CepEntity")]
        public async Task E_Possivel_Realizar_CRUD_Cep(){
        
            using(var context = _serviceProvider.GetService<MyContext>()){
                
                MunicipioImplementation _repositoryMunicipio = new MunicipioImplementation(context);
                
                MunicipioEntity _entityMunicipio = new MunicipioEntity{
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("bd08208b-bfca-47a4-9cd0-37e4e1fa5006")
                };
                
                var _registroMunicipioCriado = await _repositoryMunicipio.InsertAsync(_entityMunicipio);
                Assert.NotNull(_registroMunicipioCriado);
                Assert.True(_registroMunicipioCriado.Id != Guid.Empty);
                Assert.Equal(_registroMunicipioCriado.Nome, _entityMunicipio.Nome);
                Assert.Equal(_registroMunicipioCriado.CodIBGE, _entityMunicipio.CodIBGE);
                Assert.Equal(_registroMunicipioCriado.UfId, _entityMunicipio.UfId);


                CepImplementation _repository = new CepImplementation(context);

                CepEntity _entity = new CepEntity{
                    Logradouro = Faker.Address.StreetAddress(),
                    Numero = "0 a 2000",
                    Cep = Faker.Address.ZipCode(),
                    MunicipioId = _registroMunicipioCriado.Id
                };

                var _registroCriado = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.True(_registroCriado.Id != Guid.Empty);
                Assert.Equal(_registroCriado.Numero, _entity.Numero);
                Assert.Equal(_registroCriado.Logradouro, _entity.Logradouro);
                Assert.Equal(_registroCriado.MunicipioId, _entity.MunicipioId);
                Assert.Equal(_registroCriado.Cep, _entity.Cep);

                _entity.Cep = Faker.Address.ZipCode();
                _entity.Logradouro = Faker.Address.StreetAddress();
                _entity.Numero = "2000 a 0";

                var _registroAtualizado = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_registroCriado.Numero, _registroAtualizado.Numero);
                Assert.Equal(_registroCriado.Logradouro, _registroAtualizado.Logradouro);
                Assert.Equal(_registroCriado.MunicipioId, _registroAtualizado.MunicipioId);
                Assert.Equal(_registroCriado.Cep, _registroAtualizado.Cep);

                var _registroExiste = await _repository.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repository.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Numero, _registroAtualizado.Numero);
                Assert.Equal(_registroSelecionado.Logradouro, _registroAtualizado.Logradouro);
                Assert.Equal(_registroSelecionado.MunicipioId, _registroAtualizado.MunicipioId);
                Assert.Equal(_registroSelecionado.Cep, _registroAtualizado.Cep);
            
                _registroSelecionado = await _repository.SelectAsync(_registroAtualizado.Cep);
                Assert.NotNull(_registroSelecionado);
                Assert.NotNull(_registroSelecionado.Municipio);
                Assert.NotNull(_registroSelecionado.Municipio.Uf);

                var _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                var _apagado = await _repository.DeleteAsync(_registroSelecionado.Id);
                Assert.True(_apagado);

                _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 0);
            }
        }
    }
}