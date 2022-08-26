using System.Threading.Tasks;
using Capoeira.Persistence.Contextos;
using Capoeira.Persistence.Contratos;

namespace Capoeira.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        public readonly CapoeiraContext _context;
        public GeralPersist(CapoeiraContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

         public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}