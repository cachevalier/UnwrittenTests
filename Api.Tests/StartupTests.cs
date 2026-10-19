using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;

namespace Api.Tests;

public class StartupTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public void All_controllers_can_be_created()
    {
        using var scope = factory.Services.CreateScope();
        var partManager = scope.ServiceProvider.GetRequiredService<ApplicationPartManager>();
        var feature = new ControllerFeature();
        partManager.PopulateFeature(feature);

        foreach (var controller in feature.Controllers)
        {
            // Lève une exception si une dépendance du constructeur n'est pas enregistrée
            ActivatorUtilities.CreateInstance(scope.ServiceProvider, controller.AsType());
        }
    }

    [Fact]
    public void All_endpoints_can_be_built()
    {
        var dataSource = factory.Services.GetRequiredService<EndpointDataSource>();

        // Énumérer les endpoints force leur construction, minimal APIs comprises
        var exception = Record.Exception(() => dataSource.Endpoints.ToList());

        Assert.Null(exception);
    }

    [Fact]
    public void Invalid_configuration_prevents_startup()
    {
        using var invalidFactory = factory.WithWebHostBuilder(builder =>
            builder.ConfigureAppConfiguration(config =>
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Billing:ServiceUrl"] = ""
                })));

        Assert.Throws<OptionsValidationException>(() => invalidFactory.Server);
    }
}
