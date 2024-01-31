using System.ComponentModel.DataAnnotations;

namespace ParameterControl.Models.Policy
{
    public class Policy : GeneralData
    {
        public int Code { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripcion es requerido")]
        public string Description { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Esta conciliacion no es valida")]
        public int Conciliation { get; set; }

        public string Objetive { get; set; } = string.Empty;
    }
}
