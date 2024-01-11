using ParameterControl.Models.Rows;

namespace ParameterControl.Models.User
{
    public class TableUserViewModel: User
    {
        public List<User> Data { get; set; } = new List<User>();
        public List<Row> Rows { get; set; } = new List<Row>();
        public bool IsCreate { get; set; } = false;
        public bool IsEdit { get; set; } = false;
        public bool IsInactivate { get; set; } = false;
        public bool IsActivate { get; set; } = false;
    }
}
