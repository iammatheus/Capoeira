using Capoeira.Domain;
using Capoeira.Persistence.Contextos;
using Capoeira.Persistence.Contratos;
using Capoeira.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Capoeira.Persistence
{
    public class FiliadoPersist : IFiliadoPersist
    {
        public readonly CapoeiraContext _context;
        public FiliadoPersist(CapoeiraContext context)
        {
            _context = context;
        }

        public async Task<PageList<Filiado>> GetAllFiliadosAsync(int userId, PageParams pageParams)
        {
            IQueryable<Filiado> query = _context.Filiados;

            query = query.AsNoTracking()
                .Where(e => e.Nome.ToLower().Contains(pageParams.Term.ToLower()) &&
                             e.UserId == userId).OrderBy(e => e.Id);

            return await PageList<Filiado>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Filiado> GetFiliadoByIdAsync(int userId, int filiadoId)
        {
            IQueryable<Filiado> query = _context.Filiados;

            query = query.AsNoTracking()
                .OrderBy(e => e.Id)
                .Where(e => e.Id == filiadoId && e.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
