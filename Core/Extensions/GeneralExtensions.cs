using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class GeneralExtensions
    {
        public static T ClearCircularReference<T>(T model)
        {
            var serializeModel = JsonConvert.SerializeObject(model, Formatting.Indented,
                           new JsonSerializerSettings
                           {
                               ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                           });
            return JsonConvert.DeserializeObject<T>(serializeModel);
        }
    }
}
