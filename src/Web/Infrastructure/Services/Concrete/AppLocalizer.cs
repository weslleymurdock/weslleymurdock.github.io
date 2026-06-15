using Microsoft.Extensions.Localization;
using Web.Application.Services.Abstract;
namespace Web.Infrastructure.Services.Concrete;

public class AppLocalizer(IStringLocalizerFactory factory) : IAppLocalizer
{
    private readonly IStringLocalizer _localizer = factory.Create(
            "AppResources",
            typeof(AppLocalizer).Assembly.FullName!
        );

    public string this[string key] => _localizer[key];
}
