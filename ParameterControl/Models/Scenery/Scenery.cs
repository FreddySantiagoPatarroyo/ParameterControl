using System.ComponentModel.DataAnnotations;

namespace ParameterControl.Models.Scenery
{
    public class Scenery : GeneralData
    {
        public int Code { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = string.Empty;
        public string Impact { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo conciliación es requerido")]
        public string Conciliation { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo conciliación es requerido")]
        public int CodeConciliation { get; set; }
        public bool StateConciliation { get; set; } = false;
    }
}
