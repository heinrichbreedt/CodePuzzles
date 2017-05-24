using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGLCats.Tests
{
    public class DataProcessor
    {
        private readonly IPeopleService peopleService;

        public DataProcessor(IPeopleService peopleService)
        {
            this.peopleService = peopleService;
        }

        public async Task<List<OwnerGenderAndCats>> GetCatsByGenderOfOwner()
        {
            var people = await peopleService.Get();

            return people
                .Where(x => x.Pets != null && x.Pets.Any(p => p.Type == "Cat"))
                .GroupBy(g => g.Gender)
                .Select(g => new OwnerGenderAndCats
                {
                    Gender = g.Key,
                    CatNames = g.SelectMany(x => x.Pets).Where(x => x.Type == "Cat").Select(x => x.Name).OrderBy(x => x).ToList()
                })
                .OrderBy(x => x.Gender)
                .ToList();
        }
    }
}