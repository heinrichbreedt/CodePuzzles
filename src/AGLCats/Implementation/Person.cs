using System.Collections.Generic;
using AGLCats.Tests;

namespace AGLCats.Implementation
{
    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public List<Pet> Pets { get; set; }
    }
}