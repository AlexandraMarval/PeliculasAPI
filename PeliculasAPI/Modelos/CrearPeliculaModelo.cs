using PeliculasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class CrearPeliculaModelo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Titulo { get; set; }
        public bool EnCine { get; set; }
        public DateTime FechaEstreno { get; set; }
        [PesoArchivoValidacion(PesoMaximEnMegaBytes: 4)]
        [TipoArchivoValidacion(GrupoTipoArchivo.imagen)]
        public IFormFile Poster { get; set; }
    }
}
