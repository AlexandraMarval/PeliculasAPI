using PeliculasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class ActualizarPeliculaModelo
    {
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        [PesoArchivoValidacion(PesoMaximEnMegaBytes: 4)]
        [TipoArchivoValidacion(GrupoTipoArchivo.imagen)]
        public IFormFile Poster { get; set; }
    }
}
