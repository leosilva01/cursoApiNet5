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
    public class MunicipioCrudCompleto : DbTest, IClassFixture<DbTest>
    {
        
        private ServiceProvider _serviceProvider;
        public MunicipioCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.serviceProvider;
        }

        [Fact(DisplayName = "CRUD de Município.")]
        [Trait("CRUD", "MunicipioEntity")]
        public async Task E_Possivel_Realizar_CRUD_Municipio(){

            using(var context = _serviceProvider.GetService<MyContext>()){

                MunicipioImplementation _repository = new MunicipioImplementation(context);
                MunicipioEntity _entity = new MunicipioEntity{
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("bd08208b-bfca-47a4-9cd0-37e4e1fa5006")
                };

                var _registroCriado = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.True(_registroCriado.Id != Guid.Empty);
                Assert.Equal(_registroCriado.Nome, _entity.Nome);
                Assert.Equal(_registroCriado.CodIBGE, _entity.CodIBGE);
                Assert.Equal(_registroCriado.UfId, _entity.UfId);

                _entity.Nome = Faker.Address.City();
                _entity.CodIBGE = Faker.RandomNumber.Next(1000000, 9999999);

                var _registroAtualizado = await _repository.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_registroCriado.Nome, _registroAtualizado.Nome);
                Assert.Equal(_registroCriado.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_registroCriado.UfId, _registroAtualizado.UfId);

                var _existe = await _repository.ExistAsync(_registroAtualizado.Id);
                Assert.True(_existe);

                var _registroSelecionado = await _repository.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Nome, _registroAtualizado.Nome);
                Assert.Equal(_registroSelecionado.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_registroSelecionado.UfId, _registroAtualizado.UfId);
                Assert.Null(_registroSelecionado.Uf);


                var _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                _registroSelecionado = await _repository.GetCompleteById(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Nome, _registroAtualizado.Nome);
                Assert.Equal(_registroSelecionado.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_registroSelecionado.UfId, _registroAtualizado.UfId);
                Assert.NotNull(_registroSelecionado.Uf);

                _registroSelecionado = await _repository.GetCompleteByIBGE(_registroSelecionado.CodIBGE);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Nome, _registroAtualizado.Nome);
                Assert.Equal(_registroSelecionado.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_registroSelecionado.UfId, _registroAtualizado.UfId);
                Assert.NotNull(_registroSelecionado.Uf);

                var _apagado = await _repository.DeleteAsync(_registroSelecionado.Id);
                Assert.True(_apagado);

                _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 0);
            }
        }
    }
}