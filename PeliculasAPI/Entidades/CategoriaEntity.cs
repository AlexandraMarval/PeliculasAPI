using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class CategoriaEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
        public List<PeliculasCategorias> PeliculasCategorias { get; set; }
    }
}
