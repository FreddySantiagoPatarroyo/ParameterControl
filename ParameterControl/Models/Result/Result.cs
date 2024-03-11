namespace ParameterControl.Models.Result
{
    public class Result
    {
        public string Conciliation { get; set; } = string.Empty;
        public string Scenery { get; set; } = string.Empty;
        public string StatusConciliation { get; set; } = string.Empty;
        public string ValueBeneficiary { get; set; } = string.Empty;
        public string ValueInconsistency { get; set; } = string.Empty;
        public string ValuePqr { get; set; } = string.Empty;
        public string ValueRepetition { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.MinValue;
        public int NumberRecovered { get; set; }
        public int Value {  get; set; }

    }
}
