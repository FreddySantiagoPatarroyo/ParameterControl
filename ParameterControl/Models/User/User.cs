namespace ParameterControl.Models.User
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string CodeUser { get; set; } = string.Empty;
        public string User_ { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NameUser { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.MinValue;
        public DateTime UpdateDate { get; set; } = DateTime.MinValue; 
        public bool State { get; set; } = false;

    }
}
