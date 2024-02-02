using System.ComponentModel.DataAnnotations;

namespace ParameterControl.Models.Conciliation
{
    public class Conciliation : GeneralData
    {

        public int Code { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Por favor ingresa un correo electrónico válido.")]
        public string Email { get; set; } = string.Empty;

        public int Destination { get; set; }

        [Required(ErrorMessage = "El campo de politicas es requerido")]
        public string Policy { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es requerido")]
        public bool Required { get; set; } = false;

        [Required(ErrorMessage = "El tipo de algo es requerido")]
        public string ControlType { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de operacion es requerido")]
        public string OperationType { get; set; } = string.Empty;
    }
}
