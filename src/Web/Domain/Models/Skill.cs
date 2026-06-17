namespace Web.Domain.Models;

public class Skill
{
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public IList<string> Chips { get; set; } = [];
}