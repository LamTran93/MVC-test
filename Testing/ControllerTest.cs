using Business;
using DataAccess;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Model;
using mvc_part1.Controllers;

namespace Testing
{
    public class Tests
    {
        private RookiesController _controller;

        [SetUp]
        public void Setup()
        {
            var dao = new DataAccessObject();
            var service = new PersonService(dao);
            _controller = new RookiesController(service);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public void FullListActionTest()
        {
            var result = _controller.FullList();
            Assert.That(result, Is.TypeOf(typeof(ViewResult)), "Wrong type");
        }

        [Test]
        public void MaleActionTest()
        {
            var result = _controller.Male();
            Assert.That(result, Is.TypeOf(typeof(ViewResult)), "Wrong type");
        }

        [Test]
        public void OldestActionTest()
        {
            var result = _controller.Oldest();
            Assert.That(result, Is.TypeOf(typeof(ViewResult)), "Wrong type");
        }

        [Test]
        public void FullNameActionTest()
        {
            var result = _controller.Male();
            Assert.That(result, Is.TypeOf(typeof(ViewResult)), "Wrong type");
        }

        [TestCase(2000)]
        [TestCase(1990)]
        [TestCase(1995)]
        public void HigherActionTest(int year)
        {
            var result = _controller.Higher(year);
            Assert.That(result, Is.TypeOf(typeof(ViewResult)), "Wrong type");
            foreach (var person in ((ViewResult)result).Model as IEnumerable<ResponsePerson>)
            {
                Assert.That(person.Birthday.Year, Is.GreaterThan(year), "Wrong year");
            }
        }

        [TestCase(2000)]
        [TestCase(1990)]
        [TestCase(1995)]
        public void LowerActionTest(int year)
        {
            var result = _controller.Lower(year);
            Assert.That(result, Is.TypeOf(typeof(ViewResult)), "Wrong type");
            foreach (var person in ((ViewResult)result).Model as IEnumerable<ResponsePerson>)
            {
                Assert.That(person.Birthday.Year, Is.LessThan(year), "Wrong year");
            }
        }

        [TestCase(2000)]
        [TestCase(1990)]
        [TestCase(1995)]
        public void EqualActionTest(int year)
        {
            var result = _controller.Equal(year);
            Assert.That(result, Is.TypeOf(typeof(ViewResult)), "Wrong type");
            foreach (var person in ((ViewResult)result).Model as IEnumerable<ResponsePerson>)
            {
                Assert.That(person.Birthday.Year, Is.EqualTo(year), "Wrong year");
            }
        }

        [TestCase("2000", "higher")]
        [TestCase("1990", "lower")]
        [TestCase("1995", "equal")]
        public void AgeFilterActionTest(string year, string compare)
        {
            var result = _controller.AgeFilter(year, compare);
            Assert.That(result, Is.TypeOf(typeof(RedirectToActionResult)), "Wrong type");
            Assert.That(((RedirectToActionResult)result).ActionName.ToLower(), Is.EqualTo(compare), "Wrong action");
        }

        [Test]
        public void ExcelActionTest()
        {
            var result = _controller.Excel();
            Assert.That(result, Is.TypeOf(typeof(FileStreamResult)), "Wrong type");
            Assert.That(((FileStreamResult)result).FileDownloadName, Is.EqualTo("persons.xlsx"), "Wrong file name");
            Assert.That(((FileStreamResult)result).ContentType, Is.EqualTo("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"), "Wrong content type");
        }

        [Test]
        public void CreateActionTest()
        {
            var result = _controller.Create();
            Assert.That(result, Is.TypeOf(typeof(ViewResult)), "Wrong type");
        }

        [TestCase()]
        public void CreatePersonActionTest(
            string id,
            string firstName,
            string lastName,
            GenderType gender,
            DateOnly birthday,
            string phoneNumber,
            string birthPlace,
            bool isGraduated)
        {
            var fakePerson = new RequestPerson
            {
                Id = "1",
                FirstName = "John",
                LastName = "Doe",
                Gender = GenderType.Male,
                Birthday = new DateOnly(1990, 1, 1),
                PhoneNumber = "1234567890",
                BirthPlace = "New York",
                IsGraduated = true
            };
            var result = _controller.CreatePerson(fakePerson);
            Assert.That(result, Is.TypeOf(typeof(RedirectToActionResult)), "Wrong type");
            Assert.That(((RedirectToActionResult)result).ActionName.ToLower(), Is.EqualTo("getdetails"), "Wrong action");
        }
    }
}