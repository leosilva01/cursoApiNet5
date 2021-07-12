using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Api.Data.Test
{
    public class BaseTest
    {
        public BaseTest()
        {
            
        }
    }

    public class DbTest : IDisposable
    {
        private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider serviceProvider { get; set; }

        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o =>
                o.UseMySql($"Persist Security Info=true;Server=localhost;Database={dataBaseName};User=root;Password=mudar@123",
                    new MySqlServerVersion(new Version(8,0,23)),
                    mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)),
                    ServiceLifetime.Transient);

            serviceProvider = serviceCollection.BuildServiceProvider();

            using(var context = serviceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using(var context = serviceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    } 
}