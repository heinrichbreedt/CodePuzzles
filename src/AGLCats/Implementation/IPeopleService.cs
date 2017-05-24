using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGLCats.Tests
{
    public interface IPeopleService
    {
        Task<IEnumerable<Person>> Get();
    }
}