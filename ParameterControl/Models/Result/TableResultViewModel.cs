using ParameterControl.Models.Rows;

namespace ParameterControl.Models.Result
{
    public class TableResultViewModel
    {
        public List<Result> Data { get; set; } = new List<Result>();
        public List<Row> Rows { get; set; } = new List<Row>();
        public bool IsCreate { get; set; } = false;
        public bool IsEdit { get; set; } = false;
        public bool IsInactivate { get; set; } = false;
        public bool IsActivate { get; set; } = false;
    }
}
