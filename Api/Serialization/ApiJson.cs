using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.Serialization;

// Options de sérialisation partagées par l'API et par les tests de contrat
public static class ApiJson
{
    public static JsonSerializerOptions Options { get; } =
        Configure(new JsonSerializerOptions(JsonSerializerDefaults.Web));

    public static JsonSerializerOptions Configure(JsonSerializerOptions options)
    {
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        return options;
    }
}
