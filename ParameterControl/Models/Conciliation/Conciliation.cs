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
        public string Package { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string Policies { get; set; } = string.Empty;
        public string Required { get; set; } = string.Empty;
        public bool Result { get; set; } = false;
        public bool State { get; set; } = false;
    }
}
