using System.ComponentModel.DataAnnotations;


namespace ParameterControl.Models.Result
{
    public class Result:GeneralData
    {
        public string Id { get; set; } = string.Empty;
        public string Conciliation { get; set; } = string.Empty;
        public string Scenery { get; set; } = string.Empty;
        public string Status  { get; set; } = string.Empty;
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public string BeneValue { get; set; } = string.Empty;
        public string IncoValue { get; set; } = string.Empty;
        public string PQValue { get; set; } = string.Empty;
        public string ReinValue { get; set; } = string.Empty;
    }
}
