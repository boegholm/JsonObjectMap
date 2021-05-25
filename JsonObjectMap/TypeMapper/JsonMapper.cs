using IncludeFullJson.TypeMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace IncludeFullJson.TypeMapper
{
    public partial class JsonMapper 
    {
        public JsonSerializerSettings Settings { get; }
        public static JsonMapperBuilder Builder(JsonSerializerSettings settings) => new JsonMapperBuilder(settings);
        private JsonMapper(JsonSerializerSettings settings, Dictionary<object, JToken> map)
        {
            this.Settings = settings;
            this.Map = map;
        }
        private Dictionary<object, JToken> Map;
        public JToken Token(object t) => Map[t];
        public JToken this[object o] => Token(o);
        public bool Contains(object o) => Map.ContainsKey(o);
    }

}
