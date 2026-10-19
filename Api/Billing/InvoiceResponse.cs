namespace Api.Billing;

public sealed record InvoiceResponse(
    string Reference,
    decimal Amount,
    string Currency,
    InvoiceStatus Status);
