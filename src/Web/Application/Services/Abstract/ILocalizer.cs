namespace Web.Application.Services.Abstract;

public interface ILocalizer
{
   string this[string key] { get; }
}
