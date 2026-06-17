namespace Web.Domain.Models;
public class InOfficeExperience
{
    public string Title { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string JobDescription { get; set; } = string.Empty;
    public IList<string> Technologies { get; set; } = [];

}
