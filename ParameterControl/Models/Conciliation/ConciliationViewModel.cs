namespace ParameterControl.Models.Conciliation
{
    public class ConciliationViewModel : Conciliation
    {
        public string CodeFormat { get; set; } = string.Empty;
        public string StateFormat { get; set; } = string.Empty;
        public string RequiredFormat { get; set; } = string.Empty;
        public string CreationDateFormat { get; set; } = string.Empty;
        public string UpdateDateFormat { get; set; } = string.Empty;
    }
}
