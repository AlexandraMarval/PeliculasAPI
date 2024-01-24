using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class ActorEntidad
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string? Foto { get; set; }
        public List<PeliculasActoresEntidad> PeliculasActores { get; set; }
    }
}
