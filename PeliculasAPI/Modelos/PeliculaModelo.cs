using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class PeliculaModelo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
    }
}