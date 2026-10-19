using System.ComponentModel.DataAnnotations;

namespace Api.Billing;

public sealed class BillingOptions
{
    [Required]
    public string ServiceUrl { get; set; } = string.Empty;
}
