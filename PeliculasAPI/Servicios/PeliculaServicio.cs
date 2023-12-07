using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Servicios
{
    public class PeliculaServicio : IPeliculaServicio
    {
        private readonly IRepositorio<PeliculaEntidad> repositorio;

        public PeliculaServicio(IRepositorio<PeliculaEntidad> repositorio) 
        {
            this.repositorio = repositorio;
        }

        public Task<PeliculaModelo> ObtenerPeliculaPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
