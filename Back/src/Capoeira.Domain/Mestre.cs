using Capoeira.Domain.Identity;

namespace Capoeira.Domain
{
    public class Mestre
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public string Tipo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
