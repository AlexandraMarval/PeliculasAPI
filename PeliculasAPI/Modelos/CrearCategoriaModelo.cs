using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class CrearCategoriaModelo
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
