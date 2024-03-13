namespace ParameterControl.Audit.Entities
{
    public class AuditModel
    {
        public DateTime ModifieldDate { get; set; }
        public int UserCode { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Component { get; set; } = string.Empty;
        public string BeforeValue { get; set; } = string.Empty;
    }
}
