using System.Reflection;

namespace Web.Application.Common.Extensions;

public static class MudBlazorIconExtensions
{
    public static string ToMudBlazorSvg(this string? iconPath)
    {
        if (string.IsNullOrWhiteSpace(iconPath))
            return string.Empty;

        var parts = iconPath.Split('.');
        if (parts.Length < 2)
            return string.Empty;

        string propertyName = parts[^1];
        string typePath = string.Join(".", parts[..^1]);

        // ensures namespace
        string baseTypeName = typePath.StartsWith("MudBlazor")
            ? typePath
            : $"MudBlazor.{typePath}";
        try
        {
            var assembly = typeof(MudBlazor.Icons).Assembly;

            // attempt 1 (normal)
            var type = assembly.GetType(baseTypeName);

            // attempt 2 (nested types with '+')
            if (type == null)
            {
                var nestedName = baseTypeName.Replace(".", "+");
                type = assembly.GetType(nestedName);
            }

            // attempt 3 (safe search)
            if (type == null)
            {
                type = assembly.GetTypes()
                    .FirstOrDefault(t =>
                        t.FullName != null &&
                        (t.FullName.EndsWith(baseTypeName) ||
                         t.FullName.EndsWith(baseTypeName.Replace(".", "+"))));
            }

            if (type == null)
                return string.Empty;

            // Fields (main on MudBlazor)
            var field = type.GetField(propertyName, BindingFlags.Public | BindingFlags.Static);
            if (field != null)
                return field.GetValue(null)?.ToString() ?? string.Empty;

            // fallback: properties
           var prop = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);
            if (prop != null)
                return prop.GetValue(null)?.ToString() ?? string.Empty;
        }
        catch
        { 
        }

        return string.Empty;
    }
}