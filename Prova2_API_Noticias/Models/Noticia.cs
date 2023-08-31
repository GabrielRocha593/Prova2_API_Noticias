using System.ComponentModel.DataAnnotations;

namespace Prova2_API_Noticias.Models
{
    public class Noticia
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; } = "";
        public string? Descricao { get; set; } = "";
        public string? Chapeu { get; set; } = "";
        public DateTime DataPublicacao { get; set; }
        public string? Autor { get; set; } = "";
    }
}
