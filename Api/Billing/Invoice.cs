namespace Api.Billing;

public sealed record Invoice(decimal Amount, DateOnly IssuedOn, DateOnly DueDate);
