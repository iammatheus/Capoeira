using Capoeira.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capoeira.Domain
{
    public class Mestre
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
