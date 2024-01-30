using System.ComponentModel.DataAnnotations;

namespace ParameterControl.Models.CrossConnection
{
    public class CrossConnection
    {
        public int Code { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Status { get; set; } = string.Empty;
    }
}
