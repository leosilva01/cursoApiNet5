using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Api.Data.Context;
using Api.Data.Implementations;
using Xunit;
using System;
using Api.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Api.Data.Test
{
    public class UfGets : BaseTest, IClassFixture<DbTest>
    {
        
        private ServiceProvider _serviceProvider;
        
        public UfGets(DbTest dbTest)
        {
            _serviceProvider = dbTest.serviceProvider;
        }

        
        [Fact(DisplayName = "Gets do UF")]
        [Trait("Gets", "UfEntity")]
        public async Task E_Possivel_Realizar_Gets_Uf()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                UfImplementation _repository = new UfImplementation(context);

                UfEntity _entity = new UfEntity{
                    Id = new Guid("bd08208b-bfca-47a4-9cd0-37e4e1fa5006"),
                    Sigla = "DF",
                    Nome = "Distrito Federal"
                };

                var _registroExiste = await _repository.ExistAsync(_entity.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repository.SelectAsync(_entity.Id);
                
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Nome, _entity.Nome);
                Assert.Equal(_registroSelecionado.Sigla, _entity.Sigla);

                IEnumerable<UfEntity> _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 27);
            }
        }
    }
}