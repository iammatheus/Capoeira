using AutoMapper;
using Capoeira.Application.Contratos;
using Capoeira.Application.Dtos;
using Capoeira.Domain;
using Capoeira.Persistence.Contratos;
using Capoeira.Persistence.Models;
using System;
using System.Threading.Tasks;

namespace Capoeira.Application
{
    public class MestreService : IMestreService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IMestrePersist _mestrePersist;
        private readonly IMapper _mapper;

        public MestreService(
            IGeralPersist geralPersist,
            IMestrePersist mestrePersist,
            IMapper mapper
        )
        {
            _geralPersist = geralPersist;
            _mestrePersist = mestrePersist;
            _mapper = mapper;
        }

        public async Task<MestreDto> AddMestres(int userId, MestreDto model)
        {
            try
            {
                var evento = _mapper.Map<Mestre>(model);
                evento.UserId = userId;
                _geralPersist.Add<Mestre>(evento);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _mestrePersist.GetMestreByIdAsync(userId, evento.Id);
                    return _mapper.Map<MestreDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<MestreDto> UpdateMestre(int userId, int eventoId, MestreDto model)
        {
            try
            {
                var evento = await _mestrePersist.GetMestreByIdAsync(userId, eventoId);
                if (evento == null) return null;

                model.Id = evento.Id;
                model.UserId = userId;

                _mapper.Map(model, evento);
                _geralPersist.Update<Mestre>(evento);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _mestrePersist.GetMestreByIdAsync(userId, evento.Id);
                    return _mapper.Map<MestreDto>(eventoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteMestre(int userId, int eventoId)
        {
            try
            {
                var evento = await _mestrePersist.GetMestreByIdAsync(userId, eventoId);
                if (evento == null) throw new Exception("Erro ao excluir evento. Evento não encontrado!");

                _geralPersist.Delete<Mestre>(evento);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<MestreDto>> GetAllMestresAsync(int userId, PageParams pageParams)
        {
            try
            {
                var eventos = await _mestrePersist.GetAllMestresAsync(userId, pageParams);
                if (eventos == null) return null;

                var resultado = _mapper.Map<PageList<MestreDto>>(eventos);

                resultado.CurrentPage = eventos.CurrentPage;
                resultado.TotalPages = eventos.TotalPages;
                resultado.PageSize = eventos.PageSize;
                resultado.TotalCount = eventos.TotalCount;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MestreDto> GetMestreByIdAsync(int userId, int eventoId)
        {
            try
            {
                var evento = await _mestrePersist.GetMestreByIdAsync(userId, eventoId);
                if (evento == null) return null;

                var resultado = _mapper.Map<MestreDto>(evento);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
