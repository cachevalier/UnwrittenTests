using System.ComponentModel.DataAnnotations;
using Api.Billing;
using Xunit;

namespace Api.Tests;

public class ConfigurationTests
{
    [Theory]
    [InlineData("Staging")]
    [InlineData("Production")]
    public void Configuration_files_are_valid(string environment)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json")
            .Build();

        var options = configuration.GetSection("Billing").Get<BillingOptions>()
            ?? new BillingOptions();

        // Mêmes règles que ValidateDataAnnotations au démarrage
        Validator.ValidateObject(options, new ValidationContext(options), validateAllProperties: true);
    }
}
