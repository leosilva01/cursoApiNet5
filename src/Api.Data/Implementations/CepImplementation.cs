using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class CepImplementation : BaseRepository<CepEntity>, ICepRepository
    {
        private DbSet<CepEntity> _dataSet;
        public CepImplementation(MyContext context) : base(context)
        {
            _dataSet = context.Set<CepEntity>();
        }

        public async Task<CepEntity> SelectAsync(string cep)
        {
            return await _dataSet.Include(m => m.Municipio)
                                 .ThenInclude(u => u.Uf)
                                 .SingleOrDefaultAsync(c => c.Cep.Equals(cep));
        }
    }
}