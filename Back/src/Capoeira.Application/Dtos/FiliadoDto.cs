using System.ComponentModel.DataAnnotations;

namespace Capoeira.Application.Dtos
{
    public class FiliadoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Imagem inválida. Experimente formatos: gif, jpg, jpeg, bmp ou png.")]
        public string ImagemUrl { get; set; }

        public int UserId { get; set; }
        public UserDto UserDto { get; set; }
    }
}
