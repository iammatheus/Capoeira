using Capoeira.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capoeira.Persistence.Contratos
{
    public interface IHomePersist
    {
        public Task<Evento[]> GetAllEventosHomeAsync();
        public Task<Mestre[]> GetAllMestresHomeAsync();
    }
}
