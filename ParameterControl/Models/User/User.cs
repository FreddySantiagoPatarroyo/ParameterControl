using System.ComponentModel.DataAnnotations;

namespace ParameterControl.Models.User
{
    public class User:GeneralData
    {
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo codigo usuario es requerido")]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo usuario usuario es requerido")]
        public string User_ { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo email usuario es requerido")]
        [EmailAddress(ErrorMessage = "Este no es un correo valido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo nombre de usuario usuario es requerido")]
        public string Name { get; set; } = string.Empty;
    }
}
