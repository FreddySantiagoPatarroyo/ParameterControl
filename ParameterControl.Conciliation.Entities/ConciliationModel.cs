﻿namespace ParameterControl.Conciliation.Entities
{
    public class ConciliationModel
    {
        public int Code { get; set; }
        public string ConciliationName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TargetTable { get; set; } = string.Empty;
        public string Approval { get; set; } = string.Empty;
        public string FieldTargetTable { get; set; } = string.Empty;
        public string AssignedUser { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int PolicyId { get; set; }
        public string RequiredApproval { get; set; } = string.Empty;
        public string OperationType { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string Sox { get; set; } = string.Empty;
        public string AssignmentType { get; set; } = string.Empty;
        public string Kpi { get; set; } = string.Empty;
        public int FrequencyMonth { get; set; }
        public int Take { get; set; }
        public int Execution { get; set; }
        public int AnalysisReport { get; set; }
        public int Follow { get; set; }
        public string ScheduledDate { get; set; } = string.Empty;
        public string DeliverDate { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ModifieldDate { get; set; }
        public string TestDate { get; set; } = string.Empty;
        public string Req { get; set; } = string.Empty;
        public string PolicyName { get; set; } = string.Empty;
        public bool State { get; set; } = false;
        public bool StatePolicy { get; set; } = false;

    }
}
