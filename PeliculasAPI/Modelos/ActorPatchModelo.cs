using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class ActorPatchModelo
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
    }
}
