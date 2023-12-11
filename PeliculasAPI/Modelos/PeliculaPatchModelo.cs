using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class PeliculaPatchModelo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public bool EnCine { get; set; }
        public DateTime FechaEstreno { get; set; }
    }
}
