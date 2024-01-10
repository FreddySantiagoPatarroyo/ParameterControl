using ParameterControl.Models.Rows;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ParameterControl.Models.Parameter
{
    public class Parameter
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "El parametro es requerido")]
        public string Parameters_ { get; set; } = string.Empty;

        [Required(ErrorMessage = "El valor es requerido")]
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de parametro es requerido")]
        public string ParameterType { get; set; } = string.Empty;

        [Required(ErrorMessage = "El listado es requerido")]
        public string List {  get; set; } = string.Empty;
        public bool State { get; set; } = false;
    }
}
