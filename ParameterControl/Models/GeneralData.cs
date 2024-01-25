using System.ComponentModel.DataAnnotations;

namespace ParameterControl.Models
{
    public class GeneralData
    {
        [DataType(DataType.DateTime)]
        public DateTime UpdateDate { get; set; } = DateTime.MinValue;
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; } = DateTime.MinValue;
        public int UserOwner { get; set; }
        public bool State { get; set; } = false;
    }
}
