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

        public async Task<List<CategoriaModel>> ObtenerCategorias()
        {
            var categoria = await repositorio.ObtenerTodo();
            var categoriaModel = mapper.Map<List<CategoriaModel>>(categoria);
            return categoriaModel;
        }
        public async Task<CategoriaModel> ObtenerCategoriaPorId(int id)
        {
            var categoria = await repositorio.ObtenerPorId(id);
            var categoriaModel = mapper.Map<CategoriaModel>(categoria);
            return categoriaModel;
        }

        public async Task<CategoriaModel> CrearCategoria(CategoriaCreacionModel categoriaCreacionModel)
        {
            var categoriaExiste = await repositorio.BuscarPorCondicion(categoria => categoria.Nombre == categoriaCreacionModel.Nombre);

            if (!categoriaExiste.Any())
            {
                var categoriaCreacion = mapper.Map<CategoriaEntity>(categoriaCreacionModel);
                await repositorio.Crear(categoriaCreacion);
                var categoriaModel = mapper.Map<CategoriaModel>(categoriaCreacion);
                return categoriaModel;
            }
            else
            {
                throw new Exception("Ya existe una categoria con el mismo nombre");
            }
        }

        public async Task<CategoriaModel> ActualizarCategoria(int id, ActualizarCategoriaModelo categoriaActualizarModelo)
        {
            var categoriaPorActualizar = await repositorio.ObtenerPorId(id);

            if (categoriaPorActualizar != null)
            {
                categoriaPorActualizar.Nombre = categoriaActualizarModelo.Nombre;
                await repositorio.Actualizar(categoriaPorActualizar);
                var categoriaModelRespuesta = mapper.Map<CategoriaModel>(categoriaPorActualizar);
                return categoriaModelRespuesta;
            }
            else
            {
                throw new Exception("No existe una categoria con ese id: ");
            }
        }
    }
}
