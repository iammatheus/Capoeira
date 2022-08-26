using System.Threading.Tasks;
using Capoeira.Domain;
using Capoeira.Persistence.Models;

namespace Capoeira.Persistence.Contratos
{
    public interface IMestrePersist
    {
        Task<PageList<Mestre>> GetAllMestresAsync(int userId, PageParams pageParams);
        Task<Mestre> GetMestreByIdAsync(int userId, int mestreId);
    }
}
