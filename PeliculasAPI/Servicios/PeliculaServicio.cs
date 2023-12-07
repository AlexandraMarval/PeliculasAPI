using AutoMapper;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Servicios
{
    public class PeliculaServicio : IPeliculaServicio
    {
        private readonly IRepositorio<PeliculaEntidad> repositorio;
        private readonly IMapper mapper;

        public PeliculaServicio(IRepositorio<PeliculaEntidad> repositorio, IMapper mapper) 
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        public async Task<List<PeliculaModelo>> ObtenerPelicula()
        {
            var pelicula = await repositorio.ObtenerTodo();
            var peliculaModelo = mapper.Map<List<PeliculaModelo>>(pelicula);
            return peliculaModelo;
        }

        public async Task<PeliculaModelo> ObtenerPeliculaPorId(int id)
        {
            var pelicula = await repositorio.ObtenerPorId(id);
            var peliculaModelo = mapper.Map<PeliculaModelo>(pelicula);
            return peliculaModelo;
        }

    }
}
