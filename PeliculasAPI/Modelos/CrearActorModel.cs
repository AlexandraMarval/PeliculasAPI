using PeliculasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class CrearActorModel
    {
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        [PesoArchivoValidacion(PesoMaximEnMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.imagen)]
        public IFormFile Foto { get; set; }
    }
}
