using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using MudBlazor;
using MudBlazor.Services;
using Web.Application.Services.Abstract;
using Web.UITests.Components;
namespace Web.UITests.Core;

public abstract class TestContextBase : BunitContext
{
    protected Mock<IBrowserStorageService> SettingsServiceMock { get; } = new();
    protected Mock<ILocalizer> LocalizerMock { get; } = new();
    protected Mock<IJSRuntime> JSRuntimeMock { get; } = new();

    protected TestContextBase()
    {
        Services.AddMudServices();
        Services.AddSingleton<IDialogService>(new Mock<IDialogService>().Object);
        Services.AddSingleton<ISnackbar>(new Mock<ISnackbar>().Object);

        Services.AddSingleton(SettingsServiceMock.Object);
        Services.AddSingleton(LocalizerMock.Object);
        Services.AddSingleton(JSRuntimeMock.Object);

        // Replaces the mudblazor componentes with empty stubs
        // this blocks bunit from render attempt the real provider causing conflicts
        // satisfying the register of other components in the tree.
        ComponentFactories.AddStub<MudPopoverProvider>();
        ComponentFactories.AddStub<MudDialogProvider>();
        ComponentFactories.AddStub<MudSnackbarProvider>();
        ComponentFactories.AddStub<MudThemeProvider>();
    }
    public IRenderedComponent<T> RenderInWrapper<T>(bool isDarkMode = false) where T : IComponent
    {
        // Construímos um RenderFragment que encapsula o seu componente T
        RenderFragment componentContent = builder =>
        {
            builder.OpenComponent(0, typeof(T));
            builder.CloseComponent();
        };

        return (IRenderedComponent<T>)Render<TestWrapper>(parameters => parameters
            .Add(p => p.IsDarkMode, isDarkMode)
            .Add(p => p.ChildContent, componentContent));
    }
}