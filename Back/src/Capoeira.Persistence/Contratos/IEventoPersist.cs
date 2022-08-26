using System.Threading.Tasks;
using Capoeira.Domain;
using Capoeira.Persistence.Models;

namespace Capoeira.Persistence.Contratos
{
    public interface IEventoPersist
    {
        Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams);
        Task<Evento> GetEventoByIdAsync(int userId, int eventoId);
    }
}