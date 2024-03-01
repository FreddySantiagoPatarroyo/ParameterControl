namespace ParameterControl.Models.CrossConnection
{
    public class CrossConnectionViewModel : CrossConnection
    {
        public string StateFormat { get; set; } = string.Empty;
        public string LastLoadFormat { get; set; } = string.Empty;
        public string LastExecutionFormat { get; set; } = string.Empty;
    }
}
