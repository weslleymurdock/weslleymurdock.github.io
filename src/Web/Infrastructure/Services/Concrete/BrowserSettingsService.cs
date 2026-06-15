using Microsoft.JSInterop;

namespace Web.Infrastructure.Services.Concrete;

public sealed class BrowserSettingsService(IJSRuntime js) : IBrowserSettingsService
{
    private const string StorageKey = "wm_user_settings";

    public UserSettings Settings { get; private set; } = new();

    public event Action? OnSettingsChanged;

    public async Task<string> GetCurrentLanguageAsync(bool hard = false)
    {
        if (hard)
        {
            await ReadAsync();
        }
        return Settings.Language ?? string.Empty;
    }

    public async Task<string> GetCurrentThemeAsync(bool hard = false)
    {
        if (hard)
        {
            await ReadAsync();
        }
        return Settings.Theme ?? string.Empty;
    }

    private async Task ReadAsync()
    {
        var json = await js.InvokeAsync<string?>("localStorage.getItem", StorageKey);

        if (!string.IsNullOrEmpty(json))
        {
            try
            {
                Settings = System.Text.Json.JsonSerializer.Deserialize<UserSettings>(json) ?? new();
            }
            catch
            {
                Settings = new();
            }
        }

    }

    /// <inheritdoc />
    public async Task InitializeAsync()
    {
        await ReadAsync();

        // If theme was not manually defined, gets it from browser
        if (string.IsNullOrEmpty(Settings.Theme))
        {
            var isDarkMode = await js.InvokeAsync<bool>("eval", "window.matchMedia('(prefers-color-scheme: dark)').matches");
            Settings.Theme = isDarkMode ? "dark" : "light";
        }

        // If Locale was not manually defined, gets it from browser
        if (string.IsNullOrEmpty(Settings.Language))
        {
            string? browserLang = null;
            try
            {
                browserLang = await js.InvokeAsync<string?>("eval", "navigator.language || navigator.userLanguage");

                if (!string.IsNullOrWhiteSpace(browserLang))
                {
                    var culture = CultureInfo.GetCultureInfo(browserLang.Trim());
                    Settings.Language = culture.Name; // Salva o nome padronizado (ex: "pt-BR" ou "en-US")
                }
            }
            catch (CultureNotFoundException cnf)
            {
                if (browserLang != null && browserLang.Length >= 2)
                {
                    try
                    {
                        var shortLang = browserLang.Substring(0, 2);
                        var culture = CultureInfo.GetCultureInfo(shortLang);
                        Settings.Language = culture.Name;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex);
                        Settings.Language = "pt-BR"; // Fallback se falhar novamente
                        throw;
                    }
                }
                else
                {
                    Console.Error.WriteLine(cnf);
                    Settings.Language = "pt-BR"; // Fallback padrão caso a string seja curta/inválida
                    throw;
                }
            }
            catch (Exception ex)
            {
                // Fallback de segurança para qualquer outro erro de interoperabilidade JS
                Console.Error.WriteLine(ex);
                Settings.Language = "pt-BR";
            }
        }
    }

    /// <inheritdoc />
    public async Task UpdateLanguageAsync(string language)
    {
        Settings.Language = language;
        await SaveToStorageAsync();
        NotifyStateChanged();
    }

    /// <inheritdoc />
    public async Task UpdateThemeAsync(string theme)
    {
        Settings.Theme = theme;
        await SaveToStorageAsync();
        NotifyStateChanged();
    }

    private async Task SaveToStorageAsync()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(Settings);
        await js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }

    private void NotifyStateChanged() => OnSettingsChanged?.Invoke();
}