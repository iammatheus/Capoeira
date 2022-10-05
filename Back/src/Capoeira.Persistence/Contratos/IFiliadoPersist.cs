using Capoeira.Domain;
using Capoeira.Persistence.Models;
using System.Threading.Tasks;

namespace Capoeira.Persistence.Contratos
{
    public interface IFiliadoPersist
    {
        Task<PageList<Filiado>> GetAllFiliadosAsync(int userId, PageParams pageParams);
        Task<Filiado> GetFiliadoByIdAsync(int userId, int filiadoId);
    }
}
