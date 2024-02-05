namespace ParameterControl.Stage.Entities
{
    public class StageModel
    {
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Impact { get; set; } = string.Empty;
        public int Conciliation { get; set; }
        public bool State { get; set; } = false;
        public DateTime CreationDate { get; set; }
        public DateTime ModifieldDate { get; set; }
        public string ModifieldBy { get; set; } = string.Empty;
        public string ConciliationName { get; set; } = string.Empty;
        public bool StateConciliation { get; set; } = false;
    }
}
