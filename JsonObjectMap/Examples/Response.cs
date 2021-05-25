using System.Collections.Generic;

namespace IncludeFullJson
{
    class Response
    {
        public string Title { get; set; }
        public List<Movie> Movies { get; set; }
        public string Author { get; set; }
    }

}
