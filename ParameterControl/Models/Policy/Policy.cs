using ParameterControl.Models.Rows;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ParameterControl.Models.Policy
{
    public class Policy: GeneralData
    {
        public int Code { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripcion es requerido")]
        public string Description { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Esta conciliacion no es valida")]
        public int Conciliation { get; set; }

        [Required(ErrorMessage = "El tipo de algo es requerido")]
        public string ControlType { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de operacion es requerido")]
        public string OperationType { get; set; } = string.Empty;
    }
}
