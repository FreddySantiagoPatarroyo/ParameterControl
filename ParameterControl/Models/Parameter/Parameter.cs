using ParameterControl.Models.Rows;
using System.Reflection;

namespace ParameterControl.Models.Parameter
{
    public class Parameter
    {
        public string Id { get; set; } = string.Empty;
        public string Parameters_ { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ParameterType { get; set; } = string.Empty;
        public string List {  get; set; } = string.Empty;
        public bool State { get; set; } = false;
    }
}
