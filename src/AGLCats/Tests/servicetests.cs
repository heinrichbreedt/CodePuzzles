﻿using System.Threading.Tasks;
using AGLCats.Implementation;
using NUnit.Framework;
using Shouldly;

namespace AGLCats.Tests
{
    [TestFixture]
    public class servicetests //Test that shows how to use the actual implementation. In a real application will probably register all this with a DI framework like autofac
    {
        IPeopleService peopleService;
        DataProcessor processor;

        [SetUp]
        public void Setup()
        {
            peopleService = new ActualPeopleService();
            processor = new DataProcessor(peopleService);
        }

        [Test]
        public async Task actual_service_test()
        {
            var catsByGender = await processor.GetCatsByGenderOfOwner();
            catsByGender.Count.ShouldBe(2);

            catsByGender[0].CatNames.Count.ShouldBe(3);
            catsByGender[0].CatNames[0].ShouldBe("Garfield");
            catsByGender[0].CatNames[1].ShouldBe("Simba");
            catsByGender[0].CatNames[2].ShouldBe("Tabby");

            catsByGender[1].CatNames.Count.ShouldBe(4);
            catsByGender[1].CatNames[0].ShouldBe("Garfield");
            catsByGender[1].CatNames[1].ShouldBe("Jim");
            catsByGender[1].CatNames[2].ShouldBe("Max");
            catsByGender[1].CatNames[3].ShouldBe("Tom");
        }
    }
}