﻿namespace ParameterControl.Models.Parameter
{
    public class ParameterViewModel : Parameter
    {
        public string StateFormat { get; set; } = string.Empty;
        public string CreationDateFormat { get; set; } = string.Empty;
        public string UpdateDateFormat { get; set; } = string.Empty;
    }
}
