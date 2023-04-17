namespace weslleymurdock.github.io.Models
{
    public class Resume
    {
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public List<Education> Educations { get; set; } = new List<Education>();
        
        public Experience? Actual { get; set; }
        public List<Experience> Experiences { get; set;} = new List<Experience>();
    }

}
