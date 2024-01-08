using AutoMapper;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Servicios
{
    public class ReseñaServicio : IReseñaServicio
    {
        private readonly IRepositorio<ReseñaEntidad> repositorio;
        private readonly IMapper mapper;

        public ReseñaServicio(IRepositorio<ReseñaEntidad> repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        public Task<List<ReseñaModelo>> ObtenerTodo(int peliculaId, PaginacionModel paginacionModel)
        {
            throw new NotImplementedException();
        }
    }
}
