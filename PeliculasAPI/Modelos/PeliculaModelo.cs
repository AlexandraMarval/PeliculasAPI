using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class PeliculaModelo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool EnCine { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Poster { get; set; }
    }
}