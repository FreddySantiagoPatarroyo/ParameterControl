namespace ParameterControl.User.Entities
{
    public class UserModel
    {
        public int Code { get; set; }
        public string User { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int RolId { get; set; }
        public string GroupId { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public bool FirstAccess { get; set; }
        public string Password { get; set; } = string.Empty;
        public bool State {  get; set; }

    }
}
