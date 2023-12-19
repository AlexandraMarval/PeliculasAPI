using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;
using PeliculasAPI.Migrations;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Servicios
{
    public class PeliculaServicio : IPeliculaServicio
    {
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly IPeliculaRepositorio repositorio;
        private readonly string contenedor = "peliculas";

        public PeliculaServicio(IMapper mapper, IAlmacenadorArchivos almacenadorArchivos, IPeliculaRepositorio repositorio) 
        {
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
            this.repositorio = repositorio;
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
                //AsignarOrdenActores(crearPelicula);
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
            var peliculaDB = await repositorio.BuscarPorId(id);

            if (peliculaDB != null)
            {
                if (actualizarPeliculaModelo.Poster != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await actualizarPeliculaModelo.Poster.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(actualizarPeliculaModelo.Poster.FileName);

                        peliculaDB.Poster = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, peliculaDB.Poster,actualizarPeliculaModelo.Poster.ContentType);
                    }
                }
                AsignarOrdenActores(peliculaDB);
                await repositorio.Actualizar(peliculaDB);
                var peliculaModelo = mapper.Map<PeliculaModelo>(peliculaDB);
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

        private void AsignarOrdenActores(PeliculaEntidad peliculaEntidad)
        {
            if (peliculaEntidad.PeliculaActores != null)
            {
                for (int i = 0; i < peliculaEntidad.PeliculaActores.Count; i++)
                {
                    peliculaEntidad.PeliculaActores[i].Orden = i;
                }
            }
        }
    }
}
