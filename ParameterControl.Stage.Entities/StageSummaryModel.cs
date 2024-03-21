namespace ParameterControl.Stage.Entities
{
    public class StageSummaryModel
    {
        public int ConciliationSK {  get; set; }
        public int StageSK { get; set; }
        public string ConciliationCode { get; set; } = string.Empty;
        public string StageCode { get; set; } = string.Empty;
        public string StatusConciliation { get; set; } = string.Empty;
        public string ValueBeneficiary { get; set; } = string.Empty;
        public string ValueInconsistency { get; set; } = string.Empty;
        public string ValuePqr { get; set; } = string.Empty;
        public string ValueRepetition { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
        public int AmountBenefit { get; set; }
        public int AmountImpact { get;set; }

    }
}
