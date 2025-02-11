﻿using Capoeira.Application.Dtos;
using System.Threading.Tasks;

namespace Capoeira.Application.Contratos
{
    public interface IHomeService
    {
        Task<EventoDto[]> GetEventosHomeAsync();
        Task<MestreDto[]> GetMestresHomeAsync();
        Task<FiliadoDto[]> GetFiliadosHomeAsync();
    }
}
