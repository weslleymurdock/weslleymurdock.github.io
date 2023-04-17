namespace weslleymurdock.github.io.Models
{
    public class Experience
    {
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Start { get; set; } = string.Empty;
        public string? End{ get; set; }
        public bool IsCurrent { get; set; }
    }
}
