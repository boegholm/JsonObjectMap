using IncludeFullJson.TypeMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IncludeFullJson.Tests
{
    public class UnitTest1
    {
        static string family => JsonConvert.SerializeObject(new Family() { 
            Dad = new Person { Name = "Thomas", Dad = new Person { Name="Aage" }  } 
        });


        [Fact]
        public void TestRecursive()
        {
            Dictionary<object, JToken> data = new Dictionary<object, JToken>();
            JsonSerializerSettings s = new JsonSerializerSettings()
            {
                Converters = {new RecursiveSavingConverter<Person>(data)}
            };
            var f = JsonConvert.DeserializeObject<Family>(family, s);
            Assert.NotNull(data[f.Dad]);
            Assert.NotNull(data[f.Dad.Dad]);
        }
        [Fact]
        public void TestFlat()
        {

            JsonMapper mapper = JsonMapper.Builder(new JsonSerializerSettings())
                .Tracking<Person>()
                .Build();

            var f = JsonConvert.DeserializeObject<Family>(family, mapper.Settings);
            Assert.NotNull(mapper[f.Dad]);
            Assert.False(mapper.Contains(f.Dad.Dad));
        }
    }
}
