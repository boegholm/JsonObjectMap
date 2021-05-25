using System.Collections.Generic;

namespace IncludeFullJson
{
    class PartialMovie
    {
        public string Title { get; set; }
        public List<PartialActor> Cast { get; set; }
    }
}