﻿namespace ParameterControl.Models.Policy
{
    public class PolicyViewModel : Policy
    {
        public string CodeFormat { get; set; } = string.Empty;
        public string CreationDateFormat { get; set; } = string.Empty;
        public string UpdateDateFormat { get; set; } = string.Empty;
        public string StateFormat { get; set; } = string.Empty;

    }
}
