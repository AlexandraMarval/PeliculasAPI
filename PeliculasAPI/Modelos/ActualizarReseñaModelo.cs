using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class ActualizarReseñaModelo
    {
        public string Comentario { get; set; }
        [Range(1, 5)]
        public int Puntuacion { get; set; }
    }
}
