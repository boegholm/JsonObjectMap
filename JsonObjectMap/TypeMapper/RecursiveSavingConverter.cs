using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace IncludeFullJson
{



    public class RecursiveSavingConverter<T> : JsonConverter
    {
        public RecursiveSavingConverter(Dictionary<object, JToken> data)
        {
            this.Data = data;
        }
        Dictionary<object, JToken> Data;
        bool Enabled = true;
        public override bool CanConvert(Type objectType)
        {
            var can = typeof(T).IsAssignableFrom(objectType);
            if (!Enabled)
            {
                Enabled = can;
                return false;
            }
            else { 
                Enabled = !can;
                return can;
            }           
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try {
                JToken v = serializer.Deserialize<JToken>(reader);
                using var txtrdr = new StringReader(v.ToString());
                using var jsonrdr = new JsonTextReader(txtrdr);
                jsonrdr.Read();
                T real = serializer.Deserialize<T>(jsonrdr);
                if(real!=null)
                    Data[real] = v;
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
