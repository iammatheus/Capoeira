using Capoeira.Domain;
using System.Threading.Tasks;

namespace Capoeira.Persistence.Contratos
{
    public interface IHomePersist
    {
        public Task<Evento[]> GetAllEventosHomeAsync();
        public Task<Mestre[]> GetAllMestresHomeAsync();
        public Task<Filiado[]> GetAllFiliadosHomeAsync();
    }
}
