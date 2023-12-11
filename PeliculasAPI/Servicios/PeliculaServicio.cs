using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using PeliculasAPI.Entidades;
using PeliculasAPI.Migrations;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;
using System.Xml.XPath;

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

        public async Task<PeliculaModelo> Crear(CrearPeliculaModelo crearPeliculaModelo)
        {
            var siExistePelicula = await repositorio.BuscarPorCondicion(pelicula => pelicula.Titulo == crearPeliculaModelo.Titulo);

            if (!siExistePelicula.Any())
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

        public async Task<PeliculaModelo> ActualizarPelicula(int id, ActualizarPeliculaModelo actualizarPeliculaModelo)
        {
            var siExistePelicula = await repositorio.ObtenerPorId(id);

            if (siExistePelicula != null)
            {
                var actualizarPelicula = mapper.Map<PeliculaEntidad>(actualizarPeliculaModelo);

                if (actualizarPeliculaModelo.Poster != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await actualizarPeliculaModelo.Poster.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(actualizarPeliculaModelo.Poster.FileName);

                        actualizarPelicula.Poster = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, actualizarPeliculaModelo.Poster.ContentType);
                    }
                }
                await repositorio.Crear(actualizarPelicula);
                var peliculaModelo = mapper.Map<PeliculaModelo>(actualizarPelicula);
                return peliculaModelo;
            }
            else
            {
                throw new Exception("Ya existe una categoria con ese mismo titulo");
            }
        }

        public async Task<PeliculaPatchModelo> ActualizarPeliculaPatchId(int id, JsonPatchDocument<PeliculaPatchModelo> patchDocument)
        {
            var entidadDb = await repositorio.ObtenerPorId(id);

            if (patchDocument == null)
            {
                throw new Exception("El jsonPatchDocument es nulo");
            }

            if (entidadDb == null)
            {
                throw new Exception("No se encontró ninguna entidad con el id proporcionado");
            }
            var peliculaPatchModel = mapper.Map<PeliculaPatchModelo>(entidadDb);
            patchDocument.ApplyTo(peliculaPatchModel);

            entidadDb = mapper.Map<PeliculaEntidad>(peliculaPatchModel);

            await repositorio.Actualizar(entidadDb);

            return peliculaPatchModel;
        }

        public async Task<PeliculaModelo> Eliminar(int id)
        {
            var eliminarPelicula = await repositorio.ObtenerPorId(id);
            if (eliminarPelicula != null)
            {
                await repositorio.Elimimar(eliminarPelicula);
                var peliculaModel = mapper.Map<PeliculaModelo>(eliminarPelicula);
                return peliculaModel;
            }
            else
            {
                throw new Exception("No existe un actor por el mismo id");
            }

        }
    }
}
