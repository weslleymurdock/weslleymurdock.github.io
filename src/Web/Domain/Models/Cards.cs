namespace Web.Domain.Models;
public class Card
{
    public string title { get; set; } = string.Empty;
    public string subtitle { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string icon { get; set; } = string.Empty;
    public string[] bulletPoints { get; set; } = [];
}