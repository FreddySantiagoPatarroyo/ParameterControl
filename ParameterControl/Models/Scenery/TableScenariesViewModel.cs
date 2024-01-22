using ParameterControl.Models.Rows;

namespace ParameterControl.Models.Scenery
{
    public class TableScenariesViewModel
    {
        public List<Scenery> Data { get; set; } = new List<Scenery>();
        public List<Row> Rows { get; set; } = new List<Row>();
        public bool IsCreate { get; set; } = false;
        public bool IsEdit { get; set; } = false;
        public bool IsInactivate { get; set; } = false;
        public bool IsActivate { get; set; } = false;
    }
}
