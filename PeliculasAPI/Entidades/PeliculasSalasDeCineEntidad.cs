namespace PeliculasAPI.Entidades
{
    public class PeliculasSalasDeCineEntidad
    {
        public int PeliculaId { get; set; }
        public int SalaDeCineId { get; set; }
        public PeliculaEntidad Pelicula { get; set; }
        public SalaDeCineEntidad SalasDeCine { get; set; }
    }
}
