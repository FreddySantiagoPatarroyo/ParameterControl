namespace ParameterControl.Stage.Entities
{
    public class StageSummaryModel
    {
        public string ConciliarionCode { get; set; }
        public string StageCode { get; set; }
        public string StatusConciliation { get; set; } = string.Empty;
        public string ValueBeneficiary { get; set; } = string.Empty;
        public string ValueInconsistency { get; set; } = string.Empty;
        public string ValuePqr { get; set; } = string.Empty;
        public string ValueRepetition { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }

    }
}
