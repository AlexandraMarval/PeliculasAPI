using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class ReseñaEntidad : IId
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        [Range(1,5)]
        public int Puntuacion { get; set; }
        public int PeliculaId { get; set; }
        public PeliculaEntidad Pelicula {  get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }
    }
}
