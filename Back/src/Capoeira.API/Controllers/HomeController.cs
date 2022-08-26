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
        public async Task<IActionResult> Get()
        {
            try
            {
                var home = await _homeService.GetEventosHomeAsync();
                if (home == null) return NoContent();

                return Ok(home);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }
    }
}
