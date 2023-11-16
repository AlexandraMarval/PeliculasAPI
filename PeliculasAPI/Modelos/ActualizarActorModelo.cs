using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class ActualizarActorModelo
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public IFormFile Foto { get; set; }
    }
}
