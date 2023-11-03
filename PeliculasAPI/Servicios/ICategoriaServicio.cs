using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface ICategoriaServicio
    {
        public Task<List<CategoriaModelo>> ObtenerCategorias();
        public Task<CategoriaModelo> ObtenerCategoriaPorId(int id);
        public Task<CategoriaModelo> CrearCategoria(CrearCategoriaModelo crearCategoriaModelo);
        public Task<CategoriaModelo> ActualizarCategoria (int id, ActualizarCategoriaModelo actualizarCategoriaModelo);
        public Task<CategoriaModelo> EliminarCategoria(int id);
    }
}
