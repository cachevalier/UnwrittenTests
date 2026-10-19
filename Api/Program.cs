using Api.Billing;
using Api.Serialization;

var builder = WebApplication.CreateBuilder(args);

// L'horloge système, injectée partout où le code lit l'heure
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddOptions<BillingOptions>()
    .Bind(builder.Configuration.GetSection("Billing"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<InvoiceService>();

// Mêmes options de sérialisation pour les controllers et les minimal APIs
builder.Services.AddControllers()
    .AddJsonOptions(options => ApiJson.Configure(options.JsonSerializerOptions));
builder.Services.ConfigureHttpJsonOptions(options => ApiJson.Configure(options.SerializerOptions));

var app = builder.Build();

app.MapControllers();

app.MapGet("/invoices/preview",
    (decimal amount, InvoiceService invoiceService) => invoiceService.Create(amount));

app.Run();
