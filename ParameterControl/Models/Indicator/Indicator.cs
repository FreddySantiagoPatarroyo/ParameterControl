using System.ComponentModel.DataAnnotations;


namespace ParameterControl.Models.Indicator
{
    public class Indicator:GeneralData
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "La formula es requerida")]
        public string Formula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El escenario es requerido")]
        public string Scenery { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Parametro es requerido")]
        public string Parameter { get; set; } = string.Empty;

    }
}
