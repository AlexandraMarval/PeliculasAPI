using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Servicios
{
    public class CategoriaServicio : ICategoriaServicio
    {       
        private readonly IMapper mapper;
        private readonly IRepositorio<CategoriaEntity> repositorio;

        public CategoriaServicio(IMapper mapper, IRepositorio<CategoriaEntity> repositorio) 
        {
            this.mapper = mapper;
            this.repositorio = repositorio;
        }

        public async Task<List<CategoriaModelo>> ObtenerCategorias()
        {
            var categoria = await repositorio.ObtenerTodo();
            var categoriaModel = mapper.Map<List<CategoriaModelo>>(categoria);
            return categoriaModel;
        }
        public async Task<CategoriaModelo> ObtenerCategoriaPorId(int id)
        {
            var categoria = await repositorio.ObtenerPorId(id);
            var categoriaModel = mapper.Map<CategoriaModelo>(categoria);
            return categoriaModel;
        }

        public async Task<CategoriaModelo> CrearCategoria(CrearCategoriaModelo crearCategoriaModelo)
        {
            var categoriaExiste = await repositorio.BuscarPorCondicion(categoria => categoria.Nombre == crearCategoriaModelo.Nombre);

            if (!categoriaExiste.Any())
            {
                var crearCategoria = mapper.Map<CategoriaEntity>(crearCategoriaModelo);
                await repositorio.Crear(crearCategoria);
                var categoriaModel = mapper.Map<CategoriaModelo>(crearCategoria);
                return categoriaModel;
            }
            else
            {
                throw new Exception("Ya existe una categoria con el mismo nombre");
            }
        }

        public async Task<CategoriaModelo> ActualizarCategoria(int id, ActualizarCategoriaModelo actualizaCategoriaModelo)
        {
            var actualizarCategoria = await repositorio.ObtenerPorId(id);

            if (actualizarCategoria != null)
            {
                actualizarCategoria.Nombre = actualizaCategoriaModelo.Nombre;
                await repositorio.Actualizar(actualizarCategoria);
                var categoriaModelRespuesta = mapper.Map<CategoriaModelo>(actualizarCategoria);
                return categoriaModelRespuesta;
            }
            else
            {
                throw new Exception("No existe una categoria con ese id: ");
            }
        }

        public async Task<CategoriaModelo> EliminarCategoria(int id)
        {
            var eliminarCategoria = await repositorio.ObtenerPorId(id);

            if(eliminarCategoria != null)
            {
                await repositorio.Elimimar(eliminarCategoria);
                var categoriaModelo = mapper.Map<CategoriaModelo>(eliminarCategoria);
                return categoriaModelo;
            }
            else
            {
                throw new Exception("No existe un actor por el mismo id");
            }
        }
    }
}
