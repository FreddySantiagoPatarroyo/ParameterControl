using ParameterControl.Models.Rows;

namespace ParameterControl.Models.Conciliation
{
    public class TableConciliationViewModel
    {
        public List<Conciliation> Data { get; set; } = new List<Conciliation>();
        public List<Row> Rows { get; set; } = new List<Row>();
        public bool IsCreate { get; set; } = false;
        public bool IsEdit { get; set; } = false;
        public bool IsInactivate { get; set; } = false;
        public bool IsActivate { get; set; } = false;
    }
}
