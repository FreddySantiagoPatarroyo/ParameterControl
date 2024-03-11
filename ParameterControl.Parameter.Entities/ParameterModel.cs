namespace ParameterControl.Parameter.Entities
{
    public class ParameterModel
    {
        public int Code { get; set; }
        public string Parameter { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ModifieldDate { get; set; }
        public string ParameterType { get; set; } = string.Empty;
        public int FatherCode { get; set; }
        public string Value1 { get; set; } = string.Empty;
        public bool State { get; set; } = false;
        public string Scenary { get; set; } = string.Empty;
    }
}
