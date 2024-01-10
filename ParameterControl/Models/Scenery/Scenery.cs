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
    }
}
