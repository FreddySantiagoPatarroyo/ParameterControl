using Microsoft.AspNetCore.Mvc.Rendering;
using ParameterControl.Models.Rows;
using System.ComponentModel.DataAnnotations;

namespace ParameterControl.Models.Filter
{
    public class FilterViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public string ColumValue { get; set; } = string.Empty;
        [Required(ErrorMessage = "Este campo es requerido")]
        public string ValueFilter { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime ValueFilterDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Este campo es requerido")]
        public string ValueFilterOptions { get; set; } = string.Empty;
        public List<Row> Rows { get; set; } = new List<Row>();
        public string TypeRow { get; set; } = string.Empty;
        public List<SelectListItem> Options { get; set; } = new List<SelectListItem>();
    }
}
