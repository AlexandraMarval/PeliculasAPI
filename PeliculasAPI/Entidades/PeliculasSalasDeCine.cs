namespace PeliculasAPI.Entidades
{
    public class PeliculasSalasDeCine
    {
        public int PeliculaId { get; set; }
        public int SalaDeCineId { get; set; }
        public PeliculaEntidad Pelicula { get; set; }
        public SalaDeCine SalasDeCine { get; set; }
    }
}
