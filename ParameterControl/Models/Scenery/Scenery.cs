using ParameterControl.Models.Rows;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ParameterControl.Models.Scenery
{
    public class Scenery:GeneralData
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "El codigo es requerido")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = string.Empty;
        public string Impact { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo conciliación es requerido")]
        public string Conciliation { get; set; } = string.Empty;
    }
}
