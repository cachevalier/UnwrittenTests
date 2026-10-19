using System.Globalization;
using Api.Billing;
using Microsoft.Extensions.Time.Testing;
using Xunit;

namespace Api.Tests;

public class InvoiceServiceTests
{
    [Theory]
    [InlineData("2027-01-15T10:00:00Z", "2027-02-15")]
    [InlineData("2027-01-31T10:00:00Z", "2027-02-28")]
    [InlineData("2027-01-31T23:30:00Z", "2027-03-01")]
    [InlineData("2027-06-30T22:30:00Z", "2027-08-01")]
    public void Due_date_is_one_month_later_capped_at_month_end(
        string issuedAtUtc, string expectedDueDate)
    {
        var timeProvider = new FakeTimeProvider(
            DateTimeOffset.Parse(issuedAtUtc, CultureInfo.InvariantCulture));
        var service = new InvoiceService(timeProvider);

        var invoice = service.Create(amount: 120m);

        Assert.Equal(
            DateOnly.Parse(expectedDueDate, CultureInfo.InvariantCulture),
            invoice.DueDate);
    }
}
