namespace Web.Application.Services.Abstract;

public interface IAppLocalizer
{
   string this[string key] { get; }
}
