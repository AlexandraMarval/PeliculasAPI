using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface ICategoriaServicio
    {
        public Task<List<CategoriaModel>> ObtenerCategorias();
        public Task<CategoriaModel> ObtenerCategoriaPorId(int id);
        public Task<CategoriaModel> CrearCategoria(CategoriaCreacionModel categoriaCreacionModel);
        public Task<CategoriaModel> ActualizarCategoria (int id, ActualizarCategoriaModelo categoriaActualizarModelo);
        public Task<CategoriaModel> EliminarCategoria(int id);
    }
}
