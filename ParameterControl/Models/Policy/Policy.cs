using ParameterControl.Models.Rows;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ParameterControl.Models.Policy
{
    public class Policy
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "El codigo es requerido")]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "La descripcion es requerido")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "La conciliacion es requerido")]
        public int Conciliation { get; set; }
        [Required(ErrorMessage = "El tipo de control es requerido")]
        public string ControlType { get; set; } = string.Empty;
        [Required(ErrorMessage = "El tipo de operacion es requerido")]
        public string OperationType { get; set; } = string.Empty;
        [Required(ErrorMessage = "El estado es requerido")]
        public bool State {  get; set; } = false;

       
    }
}
