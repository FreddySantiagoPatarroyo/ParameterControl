﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterControl.Policy.Entities
{
    public class PolicyModel
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;        
        public int Conciliation { get; set; }
        public string ControlType { get; set; } = string.Empty;
        public string OperationType { get; set; } = string.Empty;
        public bool State { get; set; } = false;
        public string Objetive { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ModifieldDate { get; set; }
        public string ModifieldBy { get; set; } = string.Empty;
    }
}
