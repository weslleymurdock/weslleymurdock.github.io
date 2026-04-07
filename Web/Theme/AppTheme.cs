using MudBlazor;
namespace Web.Theme;

using MudBlazor;
public class AppTheme
{
    public static MudTheme Default => new()
    { 
        PaletteLight = new PaletteLight()
        {
            Primary = "#93C5FD",          // rgb-b
            PrimaryLighten = "#BFDBFE",
            PrimaryDarken = "#60A5FA",
            PrimaryContrastText = "#0f172a",

            Secondary = "#F9A8D4",        // cmyk-m
            SecondaryLighten = "#FBCFE8",
            SecondaryDarken = "#F472B6",
            SecondaryContrastText = "#0f172a",

            Tertiary = "#67E8F9",         // cmyk-c
            TertiaryLighten = "#A5F3FC",
            TertiaryDarken = "#22D3EE",
            TertiaryContrastText = "#0f172a",

            Background = "#0f172a",
            BackgroundGray = "#1e293b",
            Surface = "rgba(255,255,255,0.05)",

            AppbarBackground = "#0f172a",
            AppbarText = "#E5E7EB",

            DrawerBackground = "#020617",
            DrawerText = "#E5E7EB",
            DrawerIcon = "#94A3B8",

            TextPrimary = "#E5E7EB",
            TextSecondary = "#94A3B8",
            TextDisabled = "#64748B",

            Success = "#86EFAC",          // rgb-g
            SuccessLighten = "#BBF7D0",
            SuccessDarken = "#4ADE80",
            SuccessContrastText = "#022c22",

            Warning = "#FDE68A",          // cmyk-y
            WarningLighten = "#FEF3C7",
            WarningDarken = "#FACC15",
            WarningContrastText = "#422006",

            Error = "#FCA5A5",
            ErrorLighten = "#FECACA",
            ErrorDarken = "#EF4444",
            ErrorContrastText = "#450a0a",

            Info = "#67E8F9",
            InfoLighten = "#A5F3FC",
            InfoDarken = "#22D3EE",
            InfoContrastText = "#083344",

            Divider = "rgba(148,163,184,0.2)",
            DividerLight = "rgba(148,163,184,0.1)",

            TableStriped = "rgba(255,255,255,0.02)",
            TableHover = "rgba(147,197,253,0.08)",
            TableLines = "rgba(148,163,184,0.2)",

            ActionDefault = "#CBD5F5",
            ActionDisabled = "#475569",
            ActionDisabledBackground = "rgba(255,255,255,0.05)",

            HoverOpacity = 0.06,
            RippleOpacity = 0.1,
            RippleOpacitySecondary = 0.08,
            BorderOpacity = 0.12,

            Black = "#000000",
            White = "#FFFFFF",

            Dark = "#020617",
            DarkLighten = "#1e293b",
            DarkDarken = "#020617",
            DarkContrastText = "#E5E7EB"
        },

        PaletteDark = new PaletteDark()
        {
            Primary = "#60A5FA",
            Secondary = "#F472B6",
            Tertiary = "#22D3EE",

            Background = "#020617",
            BackgroundGray = "#0f172a",
            Surface = "rgba(255,255,255,0.05)",

            AppbarBackground = "#020617",
            AppbarText = "#F9FAFB",

            TextPrimary = "#F9FAFB",
            TextSecondary = "#CBD5F5",
            TextDisabled = "#64748B",

            Success = "#4ADE80",
            Warning = "#FACC15",
            Error = "#F87171",
            Info = "#38BDF8",

            Divider = "rgba(148,163,184,0.2)",
            DividerLight = "rgba(148,163,184,0.1)",

            ActionDefault = "#CBD5F5",
            ActionDisabled = "#475569",
            ActionDisabledBackground = "rgba(255,255,255,0.05)",

            HoverOpacity = 0.08,
            RippleOpacity = 0.1,
            RippleOpacitySecondary = 0.08,
            BorderOpacity = 0.12,

            Black = "#000000",
            White = "#FFFFFF",

            Dark = "#020617",
            DarkLighten = "#1e293b",
            DarkDarken = "#020617",
            DarkContrastText = "#F9FAFB"
        },

        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "14px"
        }
    };
}