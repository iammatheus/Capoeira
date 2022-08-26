using System.Threading.Tasks;
using Capoeira.Application.Dtos;
using Capoeira.Persistence.Models;

namespace Capoeira.Application.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(int userId, EventoDto model);
        Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int userId, int eventoId);

        Task<PageList<EventoDto>> GetAllEventosAsync(int userId, PageParams pageParams);
        Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId);
    }
}