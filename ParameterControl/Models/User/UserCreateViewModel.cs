using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParameterControl.Models.User
{
    public class UserCreateViewModel : User
    {
        public string CodeFormat { get; set; } = string.Empty;
        public List<SelectListItem> Roles = new List<SelectListItem>();
    }
}
