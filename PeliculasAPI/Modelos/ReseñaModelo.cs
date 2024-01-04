using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class ReseñaModelo
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public int Puntuacion { get; set; }
        public int PeliculaId { get; set; }
        public string UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
    }
}
