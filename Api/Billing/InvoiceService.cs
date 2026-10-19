namespace Api.Billing;

public sealed class InvoiceService(TimeProvider timeProvider)
{
    private static readonly TimeZoneInfo BusinessTimeZone =
        TimeZoneInfo.FindSystemTimeZoneById("Europe/Paris");

    public Invoice Create(decimal amount)
    {
        // Date d'émission dans le fuseau de l'entreprise, quel que soit celui du serveur
        var businessNow = TimeZoneInfo.ConvertTime(timeProvider.GetUtcNow(), BusinessTimeZone);
        var issuedOn = DateOnly.FromDateTime(businessNow.DateTime);
        
        // AddMonths ramène au dernier jour du mois quand le jour n'existe pas
        return new Invoice(Amount: amount, IssuedOn: issuedOn, DueDate: issuedOn.AddMonths(1));
    }
}
