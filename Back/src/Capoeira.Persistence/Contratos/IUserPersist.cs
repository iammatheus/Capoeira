using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capoeira.Domain.Identity;

namespace Capoeira.Persistence.Contratos
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUserNameAsync(string userName);
    }
}