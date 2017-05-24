using System.Collections.Generic;
using System.Threading.Tasks;
using AGLCats.Tests;

namespace AGLCats.Implementation
{
    public interface IPeopleService
    {
        Task<IEnumerable<Person>> Get();
    }
}