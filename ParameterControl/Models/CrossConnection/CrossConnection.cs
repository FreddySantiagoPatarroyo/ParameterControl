using System.ComponentModel.DataAnnotations;

namespace ParameterControl.Models.CrossConnection
{
    public class CrossConnection
    {
        [Required(ErrorMessage = "La tabla es requerido")]
        public string Table { get; set; } = string.Empty;

        [Required(ErrorMessage = "La periocidad es requerido")]
        public string Periodicity { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es requerido")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "El error es requerido")]
        public string Error { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime LastLoad { get; set; } = DateTime.MinValue;

        [DataType(DataType.Date)]
        public DateTime LastExecution { get; set; } = DateTime.MinValue;

        public bool State { get; set; } = false;
    }
}
