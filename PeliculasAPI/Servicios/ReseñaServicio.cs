using AutoMapper;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;
using System.Security.Claims;

namespace PeliculasAPI.Servicios
{
    public class ReseñaServicio : IReseñaServicio
    {
        private readonly IRepositorio<ReseñaEntidad> repositorio;
        private readonly IMapper mapper;
        private readonly IPeliculaRepositorio peliculaRepositorio;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ReseñaServicio(IRepositorio<ReseñaEntidad> repositorio, IMapper mapper, IPeliculaRepositorio peliculaRepositorio, IHttpContextAccessor httpContextAccessor)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.peliculaRepositorio = peliculaRepositorio;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ReseñaModelo> CrearUnaReseña(int peliculaId, CrearReseñaModelo crearReseñaModelo)
        {
            var existeUnaPelicula = await peliculaRepositorio.BuscarPorCondicion(pelicula => pelicula.Id == peliculaId);

            if (!existeUnaPelicula.Any())
            {
                throw new Exception("No existe reseña");
            }

            var usuarioId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(usuario => usuario.Type == ClaimTypes.NameIdentifier).Value;

            var existeUnaReseña = await repositorio.BuscarPorCondicion(reseña => reseña.PeliculaId == peliculaId && reseña.UsuarioId == usuarioId);

            if (!existeUnaReseña.Any())
            {
                throw new Exception("El usuario ya ha escrito una reseña de esta pelicula");
            }

            var reseña = mapper.Map<ReseñaEntidad>(crearReseñaModelo);
            reseña.PeliculaId = peliculaId;
            reseña.UsuarioId = usuarioId;

            repositorio.Crear(reseña);
            var reseñaModelo = mapper.Map<ReseñaModelo>(reseña);
            return reseñaModelo;
            
        }

        public async Task<List<ReseñaModelo>> ObtenerTodasLasReseñas(int peliculaId, PaginacionModel paginacionModel)
        {
            var reseñaModelo =  mapper.Map<List<ReseñaModelo>>(paginacionModel);
            return reseñaModelo;
        }
    }
}
