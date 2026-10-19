using System.Text.Json;
using Api.Billing;
using Api.Serialization;

namespace Api.Tests;

public class ContractTests
{
    [Theory]
    [InlineData(InvoiceStatus.Sent, "0")]
    [InlineData(InvoiceStatus.Paid, "1")]
    public void Invoice_status_is_serialized_as_expected(
        InvoiceStatus status, string expectedJson)
    {
        // Mêmes options de sérialisation qu'en production
        var json = JsonSerializer.Serialize(status, ApiJson.Options);

        Assert.Equal(expectedJson, json);
    }

    [Fact]
    public Task Invoice_response_contract_is_stable()
    {
        var response = new InvoiceResponse(
            Reference: "INV-0001",
            Amount: 120m,
            Currency: "EUR",
            Status: InvoiceStatus.Sent);

        var json = JsonSerializer.Serialize(response, ApiJson.Options);

        return VerifyJson(json);
    }
}
