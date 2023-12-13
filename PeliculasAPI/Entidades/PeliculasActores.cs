namespace PeliculasAPI.Entidades
{
    public class PeliculasActores
    {
        public int ActorId { get; set; }
        public int PeliculaId { get; set; }
        public string Personaje { get; set; }
        public int Orden {  get; set; }
        public ActorEntity Actor { get; set; }
        public PeliculaEntidad pelicula { get; set; }
    }
}
