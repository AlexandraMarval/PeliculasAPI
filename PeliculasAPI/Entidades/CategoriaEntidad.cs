using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class CategoriaEntidad
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        public List<PeliculasCategoriasEntidad> PeliculasCategorias { get; set; }
    }
}
