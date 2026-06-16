using Microsoft.AspNetCore.Components;

namespace Web.UITests.Components;

partial class TestWrapper : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool IsDarkMode { get; set; }
    [Parameter] public bool IsOpen { get; set; }
    [Parameter] public string Language { get; set; } = "pt-BR";
}
