using System.Text.Json;

namespace Core.Extensions
{
    public static class GeneralExtensions
    {
        public static T ClearCircularReference<T>(T model)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // JSON çıktısını düzenli yazmak için
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };

            // Serialize edip deserialize ederek circular referansı kaldırma
            var serializeModel = JsonSerializer.Serialize(model, options);
            return JsonSerializer.Deserialize<T>(serializeModel, options);
        }
    }
}
