namespace ParameterControl.Models.Policies
{
    public class Policy
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Conciliation { get; set; }
        public string ControlType { get; set; } = string.Empty;
        public string OperationType { get; set; } = string.Empty;
    }
}
