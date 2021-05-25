using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace IncludeFullJson.TypeMapper
{
    public partial class JsonMapper
    {
        public class JsonMapperBuilder
        {
            public static implicit operator JsonMapper (JsonMapperBuilder b)=>b.Build();
            private Dictionary<object, JToken> typeMap = new Dictionary<object, JToken>();
            private List<JsonConverter> typeMappers = new List<JsonConverter>();
            public JsonMapperBuilder(JsonSerializerSettings settings)
            {
                this.Settings = settings;
            }
            public JsonMapperBuilder Tracking<T>() where T : class, new()
            {
                typeMappers.Add(new FlatSavingConverter<T>(typeMap));
                return this;
            }
            public JsonMapperBuilder TrackingRecursively<T>() where T:class, new()
            {
                typeMappers.Add(new FlatSavingConverter<T>(typeMap));
                return this;
            }
            private JsonSerializerSettings Settings { get; }
            public JsonMapper Build()
            {
                List<JsonConverter> nl = new List<JsonConverter>(Settings.Converters);
                foreach (var mapper in typeMappers)
                {
                    nl.Insert(0, mapper);
                }
                Settings.Converters = nl;
                return new JsonMapper(Settings, typeMap);
            }
        }
    }

}
