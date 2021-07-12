using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Api.Data.Context
{
    //pra que essa classe é usada?
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
                        
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=mudar@123", new MySqlServerVersion(new Version(8,0,23)),
                mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend));
                
            return new MyContext(optionsBuilder.Options);
        }
    }
}