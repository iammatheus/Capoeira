using Capoeira.API.Extensions;
using Capoeira.Application.Contratos;
using Capoeira.Application.Dtos;
using Capoeira.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Capoeira.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FiliadosController : ControllerBase
    {
        private readonly IFiliadoService _filiadoService;
        private readonly IWebHostEnvironment _hostEnvironment;
    
        public FiliadosController(
            IFiliadoService filiadoService,
            IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _filiadoService = filiadoService;
    
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            try
            {
                var filiados = await _filiadoService.GetAllFiliadosAsync(User.GetUserId(), pageParams);
                if (filiados == null) return NoContent();
    
                Response.AddPagination(filiados.CurrentPage, filiados.PageSize, filiados.TotalCount, filiados.TotalPages);
    
                return Ok(filiados);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar obter filiados. Erro: {ex.Message}");
            }
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var filiado = await _filiadoService.GetFiliadoByIdAsync(User.GetUserId(), id);
                if (filiado == null) return NoContent();
                return Ok(filiado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar obter filiado. Erro: {ex.Message}");
            }
        }
    
        [HttpPost]
        public async Task<IActionResult> Post(FiliadoDto model)
        {
            try
            {
                var filiado = await _filiadoService.AddFiliado(User.GetUserId(), model);
                if (filiado == null) return NoContent();
                return Ok(filiado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar filiado. Erro: {ex.Message}");
            }
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FiliadoDto model)
        {
            try
            {
                var filiado = await _filiadoService.UpdateFiliado(User.GetUserId(), id, model);
                if (filiado == null) return NoContent();
                return Ok(filiado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar filiado. Erro: {ex.Message}");
            }
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var filiado = await _filiadoService.GetFiliadoByIdAsync(User.GetUserId(), id);
                if (filiado == null) return NoContent();
    
                if (await _filiadoService.DeleteFiliado(User.GetUserId(), id))
                {
                    DeleteImage(filiado.ImagemUrl);
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Ocorreu um problema ao deletar filiado.");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar filiado. Erro: {ex.Message}");
            }
        }
    
        [HttpPost("upload-image/{filiadoId}")]
        public async Task<IActionResult> UploadImage(int filiadoId)
        {
            try
            {
                var filiado = await _filiadoService.GetFiliadoByIdAsync(User.GetUserId(), filiadoId);
                if (filiado == null) return NoContent();
    
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(filiado.ImagemUrl);
                    filiado.ImagemUrl = await SaveImage(file);
                }
    
                var filiadoRetorno = await _filiadoService.UpdateFiliado(User.GetUserId(), filiadoId, filiado);
    
                return Ok(filiado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar filiado. Erro: {ex.Message}");
            }
        }
    
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName)
                .Take(10).ToArray()).Replace(' ', '-');
    
            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);
    
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
    
            return imageName;
        }
    
        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
