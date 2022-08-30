using Capoeira.Domain;
using Capoeira.Persistence.Contextos;
using Capoeira.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Capoeira.Persistence
{
    public class HomePersist : IHomePersist
    {
        public readonly CapoeiraContext _context;
        public HomePersist(CapoeiraContext context)
        {
            _context = context;
        }

        public async Task<Evento[]> GetAllEventosHomeAsync()
        {
            IQueryable<Evento> query = _context.Eventos;

            query = query.AsNoTracking()
                .OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Mestre[]> GetAllMestresHomeAsync()
        {
            IQueryable<Mestre> query = _context.Mestres;

            query = query.AsNoTracking()
                .OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }
    }
}
