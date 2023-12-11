using AutoMapper;
using PeliculasAPI.Entidades;
using PeliculasAPI.Migrations;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Servicios
{
    public class PeliculaServicio : IPeliculaServicio
    {
        private readonly IRepositorio<PeliculaEntidad> repositorio;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "peliculas";

        public PeliculaServicio(IRepositorio<PeliculaEntidad> repositorio, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos) 
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        public async Task<PeliculaModelo> Crear(CrearPeliculaModelo crearPeliculaModelo)
        {
            var siPeliculaExiste = await repositorio.BuscarPorCondicion(pelicula => pelicula.Titulo == crearPeliculaModelo.Titulo);

            if (!siPeliculaExiste.Any())
            {
                var crearPelicula = mapper.Map<PeliculaEntidad>(crearPeliculaModelo);

                if (crearPeliculaModelo.Poster != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await crearPeliculaModelo.Poster.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(crearPeliculaModelo.Poster.FileName);

                        crearPelicula.Poster = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, crearPeliculaModelo.Poster.ContentType);
                    }
                }
                await repositorio.Crear(crearPelicula);
                var peliculaModelo = mapper.Map<PeliculaModelo>(crearPelicula);
                return peliculaModelo;
            }
            else
            {
                throw new Exception("Ya existe una categoria con ese mismo titulo");
            }
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
