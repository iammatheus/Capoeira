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
    public class MestresController : ControllerBase
    {
        private readonly IMestreService _mestreService;
        private readonly IWebHostEnvironment _hostEnvironment;
    
        public MestresController(
            IMestreService mestreService,
            IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _mestreService = mestreService;
    
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            try
            {
                var mestres = await _mestreService.GetAllMestresAsync(User.GetUserId(), pageParams);
                if (mestres == null) return NoContent();
    
                Response.AddPagination(mestres.CurrentPage, mestres.PageSize, mestres.TotalCount, mestres.TotalPages);
    
                return Ok(mestres);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar mestre. Erro: {ex.Message}");
            }
        }
    
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var mestre = await _mestreService.GetMestreByIdAsync(User.GetUserId(), id);
                if (mestre == null) return NoContent();
                return Ok(mestre);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar mestres. Erro: {ex.Message}");
            }
        }
    
        [HttpPost]
        public async Task<IActionResult> Post(MestreDto model)
        {
            try
            {
                var mestre = await _mestreService.AddMestres(User.GetUserId(), model);
                if (mestre == null) return NoContent();
                return Ok(mestre);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar mestre. Erro: {ex.Message}");
            }
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MestreDto model)
        {
            try
            {
                var mestre = await _mestreService.UpdateMestre(User.GetUserId(), id, model);
                if (mestre == null) return NoContent();
                return Ok(mestre);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar mestre. Erro: {ex.Message}");
            }
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var mestre = await _mestreService.GetMestreByIdAsync(User.GetUserId(), id);
                if (mestre == null) return NoContent();
    
                if (await _mestreService.DeleteMestre(User.GetUserId(), id))
                {
                    DeleteImage(mestre.ImagemUrl);
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Ocorreu um problema ao deletar mestre.");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao deletar mestre. Erro: {ex.Message}");
            }
        }
    
        [HttpPost("upload-image/{mestreId}")]
        public async Task<IActionResult> UploadImage(int mestreId)
        {
            try
            {
                var mestre = await _mestreService.GetMestreByIdAsync(User.GetUserId(), mestreId);
                if (mestre == null) return NoContent();
    
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(mestre.ImagemUrl);
                    mestre.ImagemUrl = await SaveImage(file);
                }
    
                //var MestreRetorno = await _mestreService.UpdateMestre(User.GetUserId(), mestreId, mestre);
    
                return Ok(mestre);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar mestre. Erro: {ex.Message}");
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
