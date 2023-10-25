using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class CategoriaModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set;}
    }
}
