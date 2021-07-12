using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UserCrudCompleto : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public UserCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.serviceProvider;
        }

        [Fact(DisplayName = "CRUD de User")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_CRUD_User()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                UserEntity _userEntity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var _registroCriado = await _repository.InsertAsync(_userEntity);

                Assert.NotNull(_registroCriado);
                Assert.Equal(_userEntity.Email, _registroCriado.Email);
                Assert.Equal(_userEntity.Name, _registroCriado.Name);
                Assert.False(_userEntity.Id == null);

                _userEntity.Name = Faker.Name.First();
                _userEntity.Email = Faker.Internet.Email();

                var _registroAtualizado = await _repository.UpdateAsync(_userEntity);

                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_userEntity.Email, _registroAtualizado.Email);
                Assert.Equal(_userEntity.Name, _registroAtualizado.Name);

                var _registroExiste = await _repository.ExistAsync(_registroCriado.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repository.SelectAsync(_registroCriado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado, _registroSelecionado);

                var _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 1);

                var _buscaPorLogin = await _repository.FindByLogin(_registroAtualizado.Email);
                Assert.False(_buscaPorLogin == null);

                var _removeu = await _repository.DeleteAsync(_registroCriado.Id);
                Assert.True(_removeu);
                
            }
        }
    }
}