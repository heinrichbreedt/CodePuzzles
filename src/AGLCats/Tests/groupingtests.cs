using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace AGLCats.Tests
{
    [TestFixture]
    public class groupingtests
    {
        IPeopleService peopleService;
        DataProcessor processor;

        [SetUp]
        public void Setup()
        {
            peopleService = Substitute.For<IPeopleService>();
            processor = new DataProcessor(peopleService);
        }

        [Test]
        public async Task when_there_is_no_records()
        {
            peopleService.Get().Returns(new List<Person>());

            var catsByGender = await processor.GetCatsByGenderOfOwner();
            catsByGender.Count.ShouldBe(0);
        }

        [Test]
        public async Task when_there_is_1_male_with_no_cats()
        {
            peopleService.Get().Returns(new List<Person>{new Person {Gender = "Male"} });

            var catsByGender = await processor.GetCatsByGenderOfOwner();
            catsByGender.Count.ShouldBe(0);
        }

        [Test]
        public async Task when_there_is_1_male_with_1_cats()
        {
            peopleService.Get().Returns(new List<Person>{new Person {Gender = "Male", Pets = new List<Pet> {new Pet {Type = "Cat", Name = "name1"} } } });

            var catsByGender = await processor.GetCatsByGenderOfOwner();
            catsByGender.Count.ShouldBe(1);
            catsByGender[0].CatNames.Count.ShouldBe(1);
            catsByGender[0].CatNames[0].ShouldBe("name1");
        }

        [Test]
        public async Task when_there_is_2_male_with_1_cats()
        {
            var person1 = new Person {Gender = "Male", Pets = new List<Pet> {new Pet {Type = "Cat", Name = "name1"} } };
            var person2 = new Person {Gender = "Male", Pets = new List<Pet> {new Pet {Type = "Cat", Name = "name2"} } };
            peopleService.Get().Returns(new List<Person>{person1,person2 });

            var catsByGender = await processor.GetCatsByGenderOfOwner();
            catsByGender.Count.ShouldBe(1);
            catsByGender[0].CatNames.Count.ShouldBe(2);
            catsByGender[0].CatNames[0].ShouldBe("name1");
            catsByGender[0].CatNames[1].ShouldBe("name2");
        }

        [Test]
        public async Task when_there_is_2_genders_with_multiple_pets()
        {
            var person1 = new Person {Gender = "Female", Pets = new List<Pet> {new Pet {Type = "Dog", Name = "name1_1"},new Pet {Type = "Cat", Name = "name1_2"},new Pet {Type = "Cat", Name = "name1_3"}, } };
            var person2 = new Person {Gender = "Male", Pets = new List<Pet> {new Pet {Type = "Cat", Name = "name2_1"},new Pet {Type = "Cat", Name = "name2_2"},new Pet {Type = "Cat", Name = "name2_3"}, } };
            var person3 = new Person { Gender = "Female", Pets = new List<Pet> { new Pet { Type = "Cat", Name = "name3_1" }, new Pet { Type = "Dog", Name = "name3_2" }, new Pet { Type = "Cat", Name = "name3_3" }, } };
            var person4 = new Person { Gender = "Male", Pets = new List<Pet> { new Pet { Type = "Dog", Name = "name4_1" }, new Pet { Type = "Fish", Name = "name4_2" }, new Pet { Type = "Giraffe", Name = "name4_3" }, } };

            peopleService.Get().Returns(new List<Person> {person1, person2, person3, person4});

            var catsByGender = await processor.GetCatsByGenderOfOwner();
            catsByGender.Count.ShouldBe(2);
            catsByGender[0].CatNames.Count.ShouldBe(4);
            catsByGender[0].CatNames[0].ShouldBe("name1_2");
            catsByGender[0].CatNames[1].ShouldBe("name1_3");
            catsByGender[0].CatNames[2].ShouldBe("name3_1");
            catsByGender[0].CatNames[3].ShouldBe("name3_3");

            catsByGender[1].CatNames.Count.ShouldBe(3);
            catsByGender[1].CatNames[0].ShouldBe("name2_1");
            catsByGender[1].CatNames[1].ShouldBe("name2_2");
            catsByGender[1].CatNames[2].ShouldBe("name2_3");
        }

        [Test]
        public async Task when_gender_without_cats_should_not_be_in_list()
        {
            var person1 = new Person { Gender = "Male", Pets = new List<Pet> { new Pet { Type = "Dog", Name = "name1" } } };
            var person2 = new Person { Gender = "Male", Pets = new List<Pet> { new Pet { Type = "Fish", Name = "name2" } } };
            var person3 = new Person { Gender = "Female", Pets = new List<Pet> { new Pet { Type = "Cat", Name = "catwithfemale" } } };
            peopleService.Get().Returns(new List<Person> { person1, person2, person3 });

            var catsByGender = await processor.GetCatsByGenderOfOwner();
            catsByGender.Count.ShouldBe(1);
            catsByGender[0].CatNames.Count.ShouldBe(1);
            catsByGender[0].CatNames[0].ShouldBe("catwithfemale");
        }

        [Test]
        public async Task when_there_is_duplicate_names_display_them_twice() //assumes this is the business rule, otherwise add a .Distinct in Linq query
        {
            var person1 = new Person { Gender = "Male", Pets = new List<Pet> { new Pet { Type = "Cat", Name = "name1" } } };
            var person2 = new Person { Gender = "Male", Pets = new List<Pet> { new Pet { Type = "Cat", Name = "name1" } } };
            var person3 = new Person { Gender = "Female", Pets = new List<Pet> { new Pet { Type = "Cat", Name = "catwithfemale" } } };
            peopleService.Get().Returns(new List<Person> { person1, person2, person3 });

            var catsByGender = await processor.GetCatsByGenderOfOwner();
            catsByGender.Count.ShouldBe(2);
            catsByGender[0].CatNames.Count.ShouldBe(1);
            catsByGender[0].CatNames[0].ShouldBe("catwithfemale");

            catsByGender[1].CatNames.Count.ShouldBe(2);
            catsByGender[1].CatNames[0].ShouldBe("name1");
            catsByGender[1].CatNames[1].ShouldBe("name1");
        }

    }
}
