namespace ParameterControl.Models.ApprovedResult
{
    public class ApprovedResultViewModel : ApprovedResult
    {
        public string StateFormat { get; set; } = string.Empty;
        public string CreationDateFormat { get; set; } = string.Empty;
        public string UpdateDateFormat { get; set; } = string.Empty;
        public string StartDateFormat { get; set; } = string.Empty;
        public string EndDateFormat { get; set; } = string.Empty;
    }
}
