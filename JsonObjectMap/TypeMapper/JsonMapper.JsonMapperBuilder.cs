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
                typeMappers.Add(new FlatTrackerConverter<T>(typeMap));
                return this;
            }
            public JsonMapperBuilder TrackingRecursive<T>() where T:class, new()
            {
                typeMappers.Add(new RecursiveTrackerConverter<T>(typeMap));
                return this;
            }
            private JsonSerializerSettings Settings { get; }
            public JsonMapper Build()
            {
                for(int i=0;i < typeMappers.Count; i++)
                    Settings.Converters.Insert(i, typeMappers[i]);
                return new JsonMapper(Settings, typeMap);
            }
        }
    }

}
