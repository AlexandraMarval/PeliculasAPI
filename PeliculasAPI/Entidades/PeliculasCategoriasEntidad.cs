namespace PeliculasAPI.Entidades
{
    public class PeliculasCategoriasEntidad
    {
        public int CategoriaId { get; set; }
        public int PeliculaId { get; set; }
        public CategoriaEntidad Categoria { get; set; }
        public PeliculaEntidad Pelicula{ get; set; }
    }
}
