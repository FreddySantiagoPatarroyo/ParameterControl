namespace ParameterControl.Models.Audit
{
    public class Audit
    {
        public DateTime ModifieldDate { get; set; }
        public int UserCode { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Component { get; set; } = string.Empty;
        public string BeforeValue { get; set; } = string.Empty;
    }
}
