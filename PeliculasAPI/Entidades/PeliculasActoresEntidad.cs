namespace PeliculasAPI.Entidades
{
    public class PeliculasActoresEntidad
    {
        public int ActorId { get; set; }
        public int PeliculaId { get; set; }
        public string Personaje { get; set; }
        public int Orden {  get; set; }
        public ActorEntidad Actor { get; set; }
        public PeliculaEntidad Pelicula { get; set; }
    }
}
