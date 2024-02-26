namespace ParameterControl.Stage.Entities
{
    public class StageSummaryModel
    {
        public string ConciliarionCode { get; set; } = string.Empty;
        public string StageCode { get; set; } = string.Empty;
        public string StatusConciliation { get; set; } = string.Empty;
        public string ProcessDate { get; set; } = string.Empty;
        public string ProcessBeforeDate { get; set; } = string.Empty;
        public string ValueBeneficiary { get; set; } = string.Empty;
        public string ValueInconsistency { get; set; } = string.Empty;
        public string ValuePqr { get; set; } = string.Empty;
        public string ValueRepetition { get; set; } = string.Empty;
        public string StatusActivation { get; set; } = string.Empty;
        public DateTime UploadDwhDate { get; set; }
        public DateTime UpdateDwhDate { get; set; }
    }
}
