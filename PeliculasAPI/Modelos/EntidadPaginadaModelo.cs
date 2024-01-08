namespace PeliculasAPI.Modelos
{
    public class EntidadPaginadaModelo<TEntity> where TEntity : class
    {
        public double CantidadRegistros { get; set; }
        public double CantidadPaginas { get; set; }
        public int RegistrosPorPagina { get; set; }
        public int PaginaActual { get; set; }
        public IList<TEntity> Entidades { get; set; }
    }
}
