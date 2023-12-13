namespace PeliculasAPI.Entidades
{
    public class PeliculasCategorias
    {
        public int CategoriaId { get; set; }
        public int PeliculaId { get; set; }
        public CategoriaEntity Categoria { get; set; }
        public PeliculaEntidad Pelicula{ get; set; }
    }
}
