using PeliculasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class CrearActorModel : ActorPatchModelo
    {     
        [PesoArchivoValidacion(PesoMaximEnMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.imagen)]
        public IFormFile Foto { get; set; }
    }
}
