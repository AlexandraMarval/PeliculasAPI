using Microsoft.AspNetCore.JsonPatch;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface IPeliculaServicio
    {
        public Task<List<PeliculaModelo>> ObtenerPelicula();
        public Task<PeliculaModelo> ObtenerPeliculaPorId(int id);
        public Task<PeliculaModelo> Crear(CrearPeliculaModelo crearPeliculaModelo);
        public Task<PeliculaModelo> ActualizarPelicula(int id, ActualizarPeliculaModelo actualizarPeliculaModelo);
        public Task<PeliculaPatchModelo> ActualizarPeliculaPatchId(int id, JsonPatchDocument<PeliculaPatchModelo> patchDocument);
        public Task<PeliculaModelo> Eliminar(int id);
    }
}
