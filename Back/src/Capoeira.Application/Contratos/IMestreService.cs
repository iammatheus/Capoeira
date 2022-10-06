using Capoeira.Application.Dtos;
using Capoeira.Persistence.Models;
using System.Threading.Tasks;

namespace Capoeira.Application.Contratos
{
    public interface IMestreService
    {
        Task<MestreDto> AddMestres(int userId, MestreDto model);
        Task<MestreDto> UpdateMestre(int userId, int mestreId, MestreDto model);
        Task<bool> DeleteMestre(int userId, int mestreId);
        Task<PageList<MestreDto>> GetAllMestresAsync(int userId, PageParams pageParams);
        Task<MestreDto> GetMestreByIdAsync(int userId, int mestreId);
    }
}
