
using Bunit;
using Xunit;
using Moq;
using Web.UITests.Core;
using MudBlazor;

namespace Web.UITests.Layout;

public class AppBarTests : TestContextBase
{
    [Fact]
    public void AppBar_ShouldRenderTitle_FromLocalizer()
    {
        // Arrange
        LocalizerMock.Setup(l => l["ApplicationTitle"]).Returns("Meu Portfolio");

        // Act: Usamos o wrapper aqui
        var cut = RenderInWrapper<Web.Layout.AppBar>();

        // Assert
        var h3 = cut.Find("h3");
        Assert.Contains("Meu Portfolio", h3.TextContent);
    }

    [Fact]
    public void AppBar_ToggleTheme_CallsService()
    {
        // Act
        var cut = RenderInWrapper<Web.Layout.AppBar>(isDarkMode: false);

        // O clique no botão de tema
        cut.Find("button[aria-label='DarkMode']").Click();

        // Assert
        SettingsServiceMock.Verify(s => s.UpdateThemeAsync("dark"), Times.Once);
    }
}