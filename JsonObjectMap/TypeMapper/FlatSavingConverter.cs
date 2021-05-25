using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace IncludeFullJson.TypeMapper
{
    public class FlatSavingConverter<T> : JsonConverter, IJsonTypeMapper
    {
        public FlatSavingConverter(Dictionary<object, JToken> typeMap)
        {
            this.TypeMap = typeMap;
        }
        Dictionary<object, JToken> TypeMap;
        bool Enabled = true;
        public override bool CanConvert(Type objectType)
        {
            return Enabled && typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                Enabled = false;
                JToken v = serializer.Deserialize<JToken>(reader);
                using var txtrdr = new StringReader(v.ToString());
                using var jsonrdr = new JsonTextReader(txtrdr);
                jsonrdr.Read();
                T real = serializer.Deserialize<T>(jsonrdr);
                if (real != null)
                    TypeMap[real] = v;
                return real;
            }
            finally
            {
                Enabled = true;
            }
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
