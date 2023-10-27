using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class CategoriaCreacionModel
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
