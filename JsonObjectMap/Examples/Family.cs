using System.Collections.Generic;

namespace IncludeFullJson
{
    public class Family
    {
        public Person Dad { get; set; } 
        public Person Mom { get; set; }
        public List<Person> Children { get; set; }
    }

}
