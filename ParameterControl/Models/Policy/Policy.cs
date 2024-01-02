using ParameterControl.Models.Rows;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ParameterControl.Models.Policy
{
    public class Policy
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required(ErrorMessage = "El codigo es requerido")]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "La descripcion es requerido")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "La conciliacion es requerido")]
        public int Conciliation { get; set; }
        [Required(ErrorMessage = "El tipo de control es requerido")]
        public string ControlType { get; set; } = string.Empty;
        [Required(ErrorMessage = "El tipo de operacion es requerido")]
        public string OperationType { get; set; } = string.Empty;
        [Required(ErrorMessage = "El estado es requerido")]
        public bool State {  get; set; } = false;

        //Obtener listado de las propiedades de este modelo que se mostraran en la tabla
        public PropertyInfo[] GetProperties(List<Row> Columns)
        {
            PropertyInfo[] ListProperties = typeof(Policy).GetProperties();
            PropertyInfo[] DiscardProperties = typeof(Policy).GetProperties();

            //Obtener las propiedades que se descartan
            foreach (Row Column in Columns)
            {
                DiscardProperties = DiscardProperties.Where(propertie => {
                    var propertieValue = propertie?.ToString()?.Split(" ");
                    return propertieValue?[1] != Column.Value;
                }).ToArray();
            }

            //Obtener la propiedades que se van a mostrar en la tabla
            for (int i = 0; i < DiscardProperties.Length; i++)
            {
                ListProperties = ListProperties.Where((propertie) => propertie != DiscardProperties[i]).ToArray();
            }

            return ListProperties;
        }
    }
}
