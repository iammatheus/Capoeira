using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Capoeira.Domain;
using Capoeira.Persistence.Contextos;
using Capoeira.Persistence.Contratos;
using Capoeira.Persistence.Models;

namespace Capoeira.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        public readonly CapoeiraContext _context;
        public EventoPersist(CapoeiraContext context)
        {
            _context = context;
        }

        public async Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams)
        {
            IQueryable<Evento> query = _context.Eventos;

            query = query.AsNoTracking()
                .Where(e => (e.Tema.ToLower().Contains(pageParams.Term.ToLower()) || 
                             e.Local.ToLower().Contains(pageParams.Term.ToLower())) &&  
                             e.UserId == userId)
                .OrderBy(e => e.Id);

            return await PageList<Evento>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<Evento> GetEventoByIdAsync(int userId, int eventoId)
        {
            IQueryable<Evento> query = _context.Eventos;

            query = query.AsNoTracking()
                .OrderBy(e => e.Id)
                .Where(e => e.Id == eventoId && e.UserId == userId);
                
            return await query.FirstOrDefaultAsync();
        }
    }
}