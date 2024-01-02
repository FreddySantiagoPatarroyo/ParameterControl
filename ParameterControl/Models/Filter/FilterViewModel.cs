 using ParameterControl.Models.Rows;

namespace ParameterControl.Models.Filter
{
    public class FilterViewModel
    {
        public string ColumValue { get; set; } =  string.Empty;
        public string ValueFilter { get; set; } = string.Empty;
        public List<Row> Rows { get; set; } = new List<Row>();
    }
}
