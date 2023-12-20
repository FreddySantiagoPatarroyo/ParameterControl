using ParameterControl.Models.Rows;

namespace ParameterControl.Models.Filter
{
    public class FilterViewModel
    {
        public Row Filter { get; set; } =  new Row();
        public string ValueFilter { get; set; } = string.Empty;
    }
}
