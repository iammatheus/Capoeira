using System;
using System.Collections.Generic;
using Capoeira.Domain.Identity;

namespace Capoeira.Domain
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; }
        public int QtdPessoas { get; set; }
        public string ImagemUrl { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}