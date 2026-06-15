using Web.Application.Common.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IBrowserSettingsService, BrowserSettingsService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<IAppLocalizer, AppLocalizer>();
var app = builder.Build();
var sp = app.Services.CreateScope();
var bs = sp.ServiceProvider.GetRequiredService<IBrowserSettingsService>();
await bs.InitializeAsync();
var userCulture = await bs.GetUserCultureAsync();

CultureInfo ci = new(userCulture);
ci.SetCurrentCulture(userCulture);

await app.RunAsync();
