var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<IAppLocalizer, AppLocalizer>();
var app = builder.Build();
var supportedCultures = new[] { "pt-BR", "en-US" };
var defaultCulture = "pt-BR";

var userCulture = CultureInfo.CurrentUICulture.Name;
if (!supportedCultures.Contains(userCulture))
{
    userCulture = defaultCulture;
}

CultureInfo culture = new(userCulture);
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await app.RunAsync();
