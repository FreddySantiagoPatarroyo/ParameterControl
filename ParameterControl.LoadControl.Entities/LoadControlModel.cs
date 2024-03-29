﻿namespace ParameterControl.LoadControl.Entities
{
    public class LoadControlModel
    {
        public string Package { get; set; } = string.Empty;
        public string Table { get; set; } = string.Empty;
        public string Periodicity { get; set; } = string.Empty;
        public string Backup { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public DateTime LastLoad { get; set; }
        public DateTime LastExecution { get; set; }
        public string Session { get; set; } = string.Empty;
        public string LocalRouteSqlUnLoad { get; set; } = string.Empty;
        public string FlagSisNotStart { get; set; } = string.Empty;
        public string FlagSisNotOk { get; set; } = string.Empty;
        public string FlagSisNotKo { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FormulaStartDate { get; set; } = string.Empty;
        public string FormulaEndDate { get; set; } = string.Empty;
        public string FlagDrop { get; set; } = string.Empty;
        public string FlagStatistics { get; set; } = string.Empty;
        public string FlagDep { get; set; } = string.Empty;
        public int DaysDep { get; set; }
        public bool State { get; set; } = false;
    }
}
