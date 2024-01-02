using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class CredencialesUsuario
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
