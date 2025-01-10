using Serilog.Events;
using Serilog.Formatting;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Logging.SeriLog.Layouts
{
    public class JsonLayout : ITextFormatter
    {
        // Format metodu, log olayını JSON formatına dönüştürür
        public void Format(LogEvent logEvent, TextWriter writer)
        {
            // JSON serileştirme seçenekleri
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // JSON çıktısını girintili hale getir
            };

            // LogEvent'i serializable yapıya dönüştürüp JSON formatında serileştirme
            var logEventProperties = new
            {
                Timestamp = logEvent.Timestamp,
                Level = logEvent.Level.ToString(),
                Message = logEvent.MessageTemplate.Text,
                Exception = logEvent.Exception?.ToString(),
                Properties = logEvent.Properties
            };

            // Log verilerini JSON formatında serileştir
            var json = JsonSerializer.Serialize(logEventProperties, options);

            // JSON formatındaki logu yazdır
            writer.WriteLine(json);
        }
    }
}