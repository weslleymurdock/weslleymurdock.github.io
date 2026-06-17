namespace Web.Infrastructure.Services.Concrete;

public class Localizer(IStringLocalizerFactory factory) : ILocalizer
{
    private readonly IStringLocalizer _localizer = factory.Create(
            "AppResources",
            typeof(Localizer).Assembly.FullName!
        );

    public string this[string key] => _localizer[key];
}
