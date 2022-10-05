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
    public class FiliadoService : IFiliadoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IFiliadoPersist _filiadoPersist;
        private readonly IMapper _mapper;

        public FiliadoService(
            IGeralPersist geralPersist,
            IFiliadoPersist filiadoPersist,
            IMapper mapper
        )
        {
            _geralPersist = geralPersist;
            _filiadoPersist = filiadoPersist;
            _mapper = mapper;
        }

        public async Task<FiliadoDto> AddFiliado(int userId, FiliadoDto model)
        {
            try
            {
                var filiado = _mapper.Map<Filiado>(model);
                filiado.UserId = userId;
                _geralPersist.Add(filiado);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var filiadoRetorno = await _filiadoPersist.GetFiliadoByIdAsync(userId, filiado.Id);
                    return _mapper.Map<FiliadoDto>(filiadoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FiliadoDto> UpdateFiliado(int userId, int filiadoId, FiliadoDto model)
        {
            try
            {
                var filiado = await _filiadoPersist.GetFiliadoByIdAsync(userId, filiadoId);
                if (filiado == null) return null;

                model.Id = filiado.Id;
                model.UserId = userId;

                _mapper.Map(model, filiado);
                _geralPersist.Update(filiado);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var filiadoRetorno = await _filiadoPersist.GetFiliadoByIdAsync(userId, filiado.Id);
                    return _mapper.Map<FiliadoDto>(filiadoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFiliado(int userId, int filiadoId)
        {
            try
            {
                var filiado = await _filiadoPersist.GetFiliadoByIdAsync(userId, filiadoId);
                if (filiado == null) throw new Exception("Erro ao excluir filiado. Filiado não encontrado!");

                _geralPersist.Delete(filiado);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<FiliadoDto>> GetAllFiliadosAsync(int userId, PageParams pageParams)
        {
            try
            {
                var filiados = await _filiadoPersist.GetAllFiliadosAsync(userId, pageParams);
                if (filiados == null) return null;

                var resultado = _mapper.Map<PageList<FiliadoDto>>(filiados);

                resultado.CurrentPage = filiados.CurrentPage;
                resultado.TotalPages = filiados.TotalPages;
                resultado.PageSize = filiados.PageSize;
                resultado.TotalCount = filiados.TotalCount;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FiliadoDto> GetFiliadoByIdAsync(int userId, int filiadoId)
        {
            try
            {
                var filiado = await _filiadoPersist.GetFiliadoByIdAsync(userId, filiadoId);
                if (filiado == null) return null;

                var resultado = _mapper.Map<FiliadoDto>(filiado);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
