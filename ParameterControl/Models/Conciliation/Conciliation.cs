using ParameterControl.Models.Rows;
using System.Reflection;

namespace ParameterControl.Models.Conciliation
{
    public class Conciliation
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Conciliation_ { get; set; } = string.Empty;
        public bool Result { get; set; } = false;
        public bool State { get; set; } = false;

        public PropertyInfo[] GetProperties(List<Row> Columns)
        {
            PropertyInfo[] ListProperties = typeof(Conciliation).GetProperties();
            PropertyInfo[] DiscardProperties = typeof(Conciliation).GetProperties();

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
