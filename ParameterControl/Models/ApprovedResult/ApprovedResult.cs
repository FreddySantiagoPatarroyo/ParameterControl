namespace ParameterControl.Models.ApprovedResult
{
    public class ApprovedResult
    {
        public string Conciliation { get; set; } = string.Empty;
        public string Scenery { get; set; } = string.Empty;
        public string StatusConciliation { get; set; } = string.Empty;
        public string ValueBeneficiary { get; set; } = string.Empty;
        public string ValueInconsistency { get; set; } = string.Empty;
        public string ValuePqr { get; set; } = string.Empty;
        public string ValueRepetition { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.MinValue;
    }
}
