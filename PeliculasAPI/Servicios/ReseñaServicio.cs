using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Entidades;
using PeliculasAPI.Excepciones;
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

        public async Task<List<ReseñaModelo>> ObtenerReseñas(int peliculaId, PaginacionModel paginacionModel)
        {
            var existePelicula = await peliculaRepositorio.BuscarPorId(peliculaId);

            if (existePelicula == null)
            {
                throw new Exception();
            }

            var reseñaPaginadas = await repositorio.ObtenerTodoPaginado(x => true, x => x.PeliculaId.ToString(), paginacionModel);

            var reseñaModelo =  mapper.Map<List<ReseñaModelo>>(reseñaPaginadas.Entidades);
            return reseñaModelo;
        }

        public async Task<ReseñaModelo> CrearUnaReseña(int peliculaId, CrearReseñaModelo crearReseñaModelo)
        {
            var existeUnaPelicula = await peliculaRepositorio.BuscarPorId(peliculaId);

            if (existeUnaPelicula == null)
            {
                throw new Exception("No existe una pelicula");
            }

            string usuarioId = ObtenerUsuarioId();

            var existeUnaReseña = await repositorio.BuscarPorCondicion(reseña => reseña.PeliculaId == peliculaId && reseña.UsuarioId == usuarioId);

            if (existeUnaReseña.Any())
            {
                throw new Exception("El usuario ya ha escrito una reseña de esta pelicula");
            }

            var reseña = mapper.Map<ReseñaEntidad>(crearReseñaModelo);
            reseña.PeliculaId = peliculaId;
            reseña.UsuarioId = usuarioId;

            await repositorio.Crear(reseña);
            var reseñaModelo = mapper.Map<ReseñaModelo>(reseña);
            return reseñaModelo;   
        }

        public async Task<ReseñaModelo> ActualizarReseña(int peliculaId, int reseñaId, [FromBody]ActualizarReseñaModelo actualizarReseñaModelo)
        {
            var existeUnaPelicula = await peliculaRepositorio.BuscarPorCondicion(pelicula => pelicula.Id == peliculaId);

            if (!existeUnaPelicula.Any())
            {
                throw new Exception();
            }
            var reseñaDB = await repositorio.ObtenerPorId(reseñaId);
            if (reseñaDB == null) { throw new Exception(); }

            string usuarioId = ObtenerUsuarioId();

            if (reseñaDB.UsuarioId != usuarioId)
            {
                throw new ReglaDeNegocioExcepcion("No puede actualizar una reseña de otro usuario");
            }

            await repositorio.Crear(reseñaDB);
            var actualizarReseña = mapper.Map<ActualizarReseñaModelo>(reseñaDB);

            var reseñaModelo = mapper.Map<ReseñaModelo>(actualizarReseña);

            return reseñaModelo;
        }
        public async Task<ReseñaModelo> EliminarReseña(int reseñaId)
        {
            var reseñaDB = await repositorio.ObtenerPorId(reseñaId);

            if (reseñaDB == null)
            {
                throw new Exception();
            }
            string usuarioId = ObtenerUsuarioId();
            if (reseñaDB.UsuarioId != usuarioId)
            {
                throw new ReglaDeNegocioExcepcion("No puede eliminar una reseña de otro usuario");
            }
            await repositorio.Elimimar(reseñaDB);
            var reseñaModelo = mapper.Map<ReseñaModelo>(reseñaDB);
            return reseñaModelo;
        }

        private string ObtenerUsuarioId()
        {
            var claims = httpContextAccessor?.HttpContext?.User.Claims ?? throw new ReglaDeNegocioExcepcion("No hay claims para el usuario");
            var claim = claims.FirstOrDefault(usuario => usuario.Type == ClaimTypes.NameIdentifier);

            return claim?.Value ?? string.Empty;
        }

    }
}
