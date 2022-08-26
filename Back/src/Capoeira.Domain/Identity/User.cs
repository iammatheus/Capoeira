using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Capoeira.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string Nome { get; set; }
        public string ImagemURL { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}