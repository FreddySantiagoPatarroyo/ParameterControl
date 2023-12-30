using ParameterControl.Models.Rows;
using System.Reflection;

namespace ParameterControl.Models.Scenery
{
    public class Scenery
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Impact { get; set; } = string.Empty;
        public string Conciliation { get; set; } = string.Empty;
        public string Query { get; set; } = string.Empty;
        public string Parameter { get; set; } = string.Empty;
        public bool State { get; set; } = false;

        //Obtener listado de las propiedades de este modelo que se mostraran en la tabla
        public PropertyInfo[] GetProperties(List<Row> Columns)
        {
            PropertyInfo[] ListProperties = typeof(Scenery).GetProperties();
            PropertyInfo[] DiscardProperties = typeof(Scenery).GetProperties();

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
