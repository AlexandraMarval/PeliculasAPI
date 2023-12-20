using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class PeliculaEntidad
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public bool EnCine { get; set; }
        public DateTime FechaEstreno{ get; set; }
        public string Poster { get; set; }
        public List<PeliculasActoresEntidad> PeliculaActores { get; set; }
        public List<PeliculasCategoriasEntidad> PeliculaCategorias { get; set; }
        public List<PeliculasSalasDeCineEntidad> peliculasSalasDeCines { get; set; }
    }
}
