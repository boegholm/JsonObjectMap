using IncludeFullJson.TypeMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

namespace IncludeFullJson
{
    class Program
    {
        static void Main(string[] args)
        {

            var res = new Response
            {
                Author = "Thomas",
                Title = "Action Movie Collection",
                Movies = new List<Movie>{
                    new Movie {
                        Title ="Terminator 2: Judgment Day",
                        Year = 1991,
                        Cast=new List<Actor>
                        {
                            new Actor{Name="Arnold Schwarzenegger", Born=1947, Nickname="The Austrian Oak"},
                            new Actor{Name="Linda Hamilton", Born=1956 }
                        }
                    }
                }
            };
            var serializedResponse = JsonConvert.SerializeObject(res, Formatting.Indented);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            JsonMapper map = JsonMapper.Builder(settings)
                .Tracking<Movie>()
                .Tracking<Actor>()
                .Build()
                ;
            var resp = JsonConvert.DeserializeObject<Response>(serializedResponse, map.Settings);
            Movie terminator = resp.Movies[0];
            Actor arnold = terminator.Cast[0];
            Actor linda = terminator.Cast[1];
            Console.WriteLine(map[arnold]);
            Console.WriteLine(map[linda]);

        }
    }
}
