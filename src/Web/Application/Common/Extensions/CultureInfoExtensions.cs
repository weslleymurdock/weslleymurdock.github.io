namespace Web.Application.Common.Extensions;

public static class CultureInfoExtensions
{
    public static string[] SupportedCultures { get => ["pt-BR", "en-US"]; }
    extension(CultureInfo ci)
    {

        public void SetCurrentCulture(string userCulture)
        {
            var defaultCulture = SupportedCultures[0];
            userCulture ??= CultureInfo.CurrentUICulture.Name;
            if (!SupportedCultures.Contains(userCulture))
            {
                userCulture = defaultCulture;
            }

            ci = new(userCulture);
            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;
        }
    }
}
