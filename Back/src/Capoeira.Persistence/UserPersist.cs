using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Capoeira.Domain.Identity;
using Capoeira.Persistence.Contextos;
using Capoeira.Persistence.Contratos;

namespace Capoeira.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly CapoeiraContext _context;
        public UserPersist(CapoeiraContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }
    }
}