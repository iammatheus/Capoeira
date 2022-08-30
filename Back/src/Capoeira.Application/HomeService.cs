using AutoMapper;
using Capoeira.Application.Contratos;
using Capoeira.Application.Dtos;
using Capoeira.Domain;
using Capoeira.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capoeira.Application
{
    public class HomeService : IHomeService
    {
        private readonly IHomePersist _homePersist;
        private readonly IMapper _mapper;

        public HomeService(
            IHomePersist homePersist,
            IMapper mapper
        )
        {
            _homePersist = homePersist;
            _mapper = mapper;
        }

        public async Task<EventoDto[]> GetEventosHomeAsync()
        {
            try
            {
                var eventos = await _homePersist.GetAllEventosHomeAsync();
                if (eventos == null) return null;

                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<MestreDto[]> GetMestresHomeAsync()
        {
            try
            {
                var mestres = await _homePersist.GetAllMestresHomeAsync();
                if (mestres == null) return null;

                var resultado = _mapper.Map<MestreDto[]>(mestres);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
