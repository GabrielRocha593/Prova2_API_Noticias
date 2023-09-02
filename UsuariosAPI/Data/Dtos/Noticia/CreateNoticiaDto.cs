using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.Dtos.Noticia
{
    public class CreateNoticiaDto
    {
        [Required(ErrorMessage = "O campo de Titulo é obrigatório.")]
        public string Titulo { get; set; } = "";
        public string? Descricao { get; set; } = "";
        public string? Chapeu { get; set; } = "";
        public string? Autor { get; set; } = "";

    }
}
