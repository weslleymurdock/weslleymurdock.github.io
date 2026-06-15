namespace Web.Domain.Models;
public class UserSettings
{
    public string? Theme { get; set; } // "light" ou "dark"
    public string? Language { get; set; } // ex: "pt-BR", "en"
}