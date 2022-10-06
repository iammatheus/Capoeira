using System.ComponentModel.DataAnnotations;

namespace Capoeira.Application.Dtos
{
    public class MestreDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        StringLength(50, MinimumLength = 3, ErrorMessage = "Insira de 3 a 50 caracteres.")]
        public string Nome { get; set; }

        [StringLength(500, MinimumLength = 0, ErrorMessage = "Insira de 3 a 50 caracteres.")]
        public string Descricao { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Imagem inválida. Experimente formatos: gif, jpg, jpeg, bmp ou png.")]
        public string ImagemUrl { get; set; }

        public int UserId { get; set; }
        public UserDto UserDto { get; set; }
    }
}
