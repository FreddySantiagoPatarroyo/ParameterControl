using System.ComponentModel.DataAnnotations;

namespace ParameterControl.Models.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string user { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string password { get; set; } = string.Empty;
    }
}
