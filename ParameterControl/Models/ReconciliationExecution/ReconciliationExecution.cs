using System.ComponentModel.DataAnnotations;


namespace ParameterControl.Models.ReconciliationExecution
{
    public class ReconciliationExecution
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Conciliation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Por favor seleccione la fecha de ejecución")]
        public DateTime DateExecution { get; set; } = DateTime.MinValue;


    }
}
