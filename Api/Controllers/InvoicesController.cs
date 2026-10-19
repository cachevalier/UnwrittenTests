using System.Globalization;
using Api.Billing;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("invoices")]
public sealed class InvoicesController(InvoiceService invoiceService) : ControllerBase
{
    [HttpPost]
    public ActionResult<InvoiceResponse> Create(CreateInvoiceRequest request)
    {
        var invoice = invoiceService.Create(request.Amount);

        return new InvoiceResponse(
            Reference: string.Create(CultureInfo.InvariantCulture, $"INV-{invoice.IssuedOn:yyyyMMdd}"),
            Amount: invoice.Amount,
            Currency: "EUR",
            Status: InvoiceStatus.Sent);
    }
}

public sealed record CreateInvoiceRequest(decimal Amount);
