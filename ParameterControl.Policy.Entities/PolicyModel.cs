namespace ParameterControl.Policy.Entities
{
    public class PolicyModel
    {
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool State { get; set; } = false;
        public string Objetive { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ModifieldDate { get; set; }
        public string Package { get; set; } = string.Empty; 
    }
}
