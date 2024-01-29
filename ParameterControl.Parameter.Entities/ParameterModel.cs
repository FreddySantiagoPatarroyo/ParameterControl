using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterControl.Parameter.Entities
{
    public class ParameterModel
    {
        public string Id { get; set; } = string.Empty;
        public string Parameter { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ModifieldDate { get; set; }
        public string Type { get; set; } = string.Empty;
        public int FatherId { get; set; }
        public string Value1 { get; set; } = string.Empty;
        public string ModifieldBy { get; set; } = string.Empty;
    }
}
