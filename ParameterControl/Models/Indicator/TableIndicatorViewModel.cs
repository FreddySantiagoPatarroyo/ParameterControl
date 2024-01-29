using ParameterControl.Models.Rows;

namespace ParameterControl.Models.Indicator
{
    public class TableIndicatorViewModel
    {
        public List<IndicatorViewModel> Data { get; set; } = new List<IndicatorViewModel>();
        public List<Row> Rows { get; set; } = new List<Row>();
        public bool IsCreate { get; set; } = false;
        public bool IsEdit { get; set; } = false;
        public bool IsInactivate { get; set; } = false;
        public bool IsActivate { get; set; } = false;
        public bool Filter { get; set; } = false;
    }
}
