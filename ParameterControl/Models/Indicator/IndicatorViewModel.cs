namespace ParameterControl.Models.Indicator
{
    public class IndicatorViewModel:Indicator
    {
        public string StateFormat { get; set; } = string.Empty;
        public string CreationDateFormat { get; set; } = string.Empty;
        public string UpdateDateFormat { get; set; } = string.Empty;
    }
}
