using System.ComponentModel.DataAnnotations;
using modConiliation = ParameterControl.Models.Conciliation;

namespace ParameterControl.Models.ConciliationExecution
{
    public class ConciiliationExecution
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int ConciliationCode { get; set; }

        [Required(ErrorMessage = "Por favor seleccione la fecha de ejecución")]
        public DateTime DateExecution { get; set; } = DateTime.MinValue;

        public modConiliation.Conciliation conciliation { get; set; }
    }
}
