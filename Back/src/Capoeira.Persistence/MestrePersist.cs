using Capoeira.Domain;
using Capoeira.Persistence.Contextos;
using Capoeira.Persistence.Contratos;
using Capoeira.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Capoeira.Persistence
{
    public class MestrePersist : IMestrePersist
    {
        public readonly CapoeiraContext _context;
        public MestrePersist(CapoeiraContext context)
        {
            _context = context;
        }

        public async Task<PageList<Mestre>> GetAllMestresAsync(int userId, PageParams pageParams)
        {
            IQueryable<Mestre> query = _context.Mestres;

            query = query.AsNoTracking()
                .Where(e => e.Nome.ToLower().Contains(pageParams.Term.ToLower()) || e.Tipo.Contains(pageParams.Term) &&
                             e.UserId == userId).OrderBy(e => e.Id);

            return await PageList<Mestre>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Mestre> GetMestreByIdAsync(int userId, int mestreId)
        {
            IQueryable<Mestre> query = _context.Mestres;

            query = query.AsNoTracking()
                .OrderBy(e => e.Id)
                .Where(e => e.Id == mestreId && e.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
