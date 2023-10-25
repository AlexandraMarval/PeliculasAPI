using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface ICategoriaServicio
    {
        public Task<CategoriaModel> ObtenerCategoria();
    }
}
