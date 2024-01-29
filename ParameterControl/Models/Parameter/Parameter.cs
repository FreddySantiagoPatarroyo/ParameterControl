using ParameterControl.Models.Rows;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ParameterControl.Models.Parameter
{
    public class Parameter:GeneralData
    {
        public int Code { get; set; }

        [Required(ErrorMessage = "El parametro es requerido")]
        public string Parameter_ { get; set; } = string.Empty;

        [Required(ErrorMessage = "El valor es requerido")]
        public string Value { get; set; } = string.Empty;

        [Required(ErrorMessage = "La description es requerido")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de parametro es requerido")]
        public string ParameterType { get; set; } = string.Empty;

        [Required(ErrorMessage = "El listado es requerido")]
        public string List {  get; set; } = string.Empty;
    }
}
