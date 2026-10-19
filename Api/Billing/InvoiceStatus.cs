namespace Api.Billing;

// Sérialisé en nombre par défaut : ajouter une valeur en première position
// (par exemple Draft) décale toutes les suivantes. Les tests de contrat le détectent.
public enum InvoiceStatus
{
    Sent,
    Paid
}
