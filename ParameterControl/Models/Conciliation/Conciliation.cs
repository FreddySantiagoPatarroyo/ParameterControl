using ParameterControl.Models.Rows;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ParameterControl.Models.Conciliation
{
    public class Conciliation
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "El codigo es requerido")]
        public string Code { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = string.Empty;
       
        public string Description { get; set; } = string.Empty;
        public string Conciliation_ { get; set; } = string.Empty;

        [Required(ErrorMessage = "El paquete es requerido")]
        public string Package { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Por favor ingresa un correo electrónico válido.")]
        public string Email { get; set; } = string.Empty;
       
        public string Destination { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo de politicas es requerido")]
        public string Policies { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Required { get; set; } = string.Empty;
        
        public bool State { get; set; } = false;
    }
}
