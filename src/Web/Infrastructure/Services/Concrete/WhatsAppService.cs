using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace Web.Infrastructure.Services.Concrete;

public class WhatsAppService(NavigationManager Navigation) : IWhatsAppService
{
    public void OpenWhatsApp(string message, string number)
    {
        string encoded = Uri.EscapeDataString(message);
        string url = $"https://wa.me/{number}?text={encoded}";
        Navigation.NavigateTo(url);
    }
     
}