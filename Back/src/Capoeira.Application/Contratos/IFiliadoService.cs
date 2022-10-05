using Capoeira.Application.Dtos;
using Capoeira.Persistence.Models;
using System.Threading.Tasks;

namespace Capoeira.Application.Contratos
{
    public interface IFiliadoService
    {
        Task<FiliadoDto> AddFiliado(int userId, FiliadoDto model);
        Task<FiliadoDto> UpdateFiliado(int userId, int eventoId, FiliadoDto model);
        Task<bool> DeleteFiliado(int userId, int eventoId);
        Task<PageList<FiliadoDto>> GetAllFiliadosAsync(int userId, PageParams pageParams);
        Task<FiliadoDto> GetFiliadoByIdAsync(int userId, int eventoId);
    }
}
