namespace ParameterControl.Consistency.Entities
{
    public class ConsistencyModel
    {
        public int Code { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public int Otmerch { get; set; }
        public bool Status { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime SuspensionDate { get; set; }
        public int Cfm { get; set; }
        public int Cycle { get; set; }
        public int Difference { get; set; }
        public int Difference1 { get; set; }
        public string ConciliationCode { get; set; } = string.Empty;
        public string StageCode { get; set; } = string.Empty;
        public int Repetition { get; set; }
        public DateTime RepetitionDate { get; set; }
        public int Pqr { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
