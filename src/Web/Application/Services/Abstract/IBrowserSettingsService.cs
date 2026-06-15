namespace Web.Application.Services.Abstract;

public interface IBrowserSettingsService
{

    /// <summary>
    /// Retrieves the current theme saved in browser settings
    /// </summary>
    /// <returns>a string representing the theme: e.g. : 'light', 'dark'</returns>
    Task<string> GetThemesAsync(bool hard = false);

    /// <summary>
    /// Retrieves the language stored in browser settings
    /// </summary>
    /// <returns>string representing language of pages</returns>
    Task<string> GetUserCultureAsync(bool hard = false);

    /// <summary>
    /// Init the settings searching
    /// from localstorage LocalStorage or detecting system preferences
    /// </summary>
    Task InitializeAsync();

    event Action? OnSettingsChanged;
    UserSettings Settings { get; }
    /// <summary>
    /// Sets or updates a user defined locale.
    /// </summary>
    /// <param name="language">a 5 digit culture string</param>
    Task UpdateLanguageAsync(string language);
    
    /// <summary>
    /// Sets or updates a user defined theme.
    /// </summary>
    /// <param name="theme">a theme mark string of desired theme (e.g. 'light', 'dark').</param>
    Task UpdateThemeAsync(string theme);
}