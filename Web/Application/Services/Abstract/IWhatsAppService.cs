namespace Web.Application.Services.Abstract;

public interface IWhatsAppService
{
    void OpenWhatsApp(string message, string number);
}