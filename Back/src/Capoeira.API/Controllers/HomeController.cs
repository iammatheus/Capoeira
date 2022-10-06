using Capoeira.Application.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Capoeira.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        public IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        [Route("eventos")]
        public async Task<IActionResult> GetEventos()
        {
            try
            {
                var eventos = await _homeService.GetEventosHomeAsync();
                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("mestres")]
        public async Task<IActionResult> GetMestres()
        {
            try
            {
                var mestres = await _homeService.GetMestresHomeAsync();
                if (mestres == null) return NoContent();

                return Ok(mestres);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar mestres. Erro: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("filiados")]
        public async Task<IActionResult> GetFiliados()
        {
            try
            {
                var filiados = await _homeService.GetFiliadosHomeAsync();
                if (filiados == null) return NoContent();

                return Ok(filiados);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar carregar filiados. Erro: {ex.Message}");
            }
        }
    }
}
