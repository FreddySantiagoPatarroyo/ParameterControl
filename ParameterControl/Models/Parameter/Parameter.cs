using ParameterControl.Models.Rows;
using System.Reflection;

namespace ParameterControl.Models.Parameter
{
    public class Parameter
    {
        public string Id { get; set; } = string.Empty;
        public string _Parameters { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ParameterType { get; set; } = string.Empty;
        public bool State { get; set; } = false;

        //Obtener listado de las propiedades de este modelo que se mostraran en la tabla
        public PropertyInfo[] GetProperties(List<Row> Columns)
        {
            PropertyInfo[] ListProperties = typeof(Parameter).GetProperties();
            PropertyInfo[] DiscardProperties = typeof(Parameter).GetProperties();

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
