using Business;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Model;
using mvc_part1.Controllers;

namespace Testing.ControllerTests
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
            var result = _controller.FullName();
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

        [TestCase("1", "Lam", "Tran", GenderType.Male, "23/2/2003", "0123456789", "Ha Noi", true)]
        public void CreatePersonActionTest(
            string id,
            string firstName,
            string lastName,
            GenderType gender,
            string birthday,
            string phoneNumber,
            string birthPlace,
            bool isGraduated
            )
        {
            var fakePerson = new RequestPerson
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Birthday = DateOnly.Parse(birthday),
                PhoneNumber = phoneNumber,
                BirthPlace = birthPlace,
                IsGraduated = isGraduated
            };
            var result = _controller.CreatePerson(fakePerson);
            Assert.That(result, Is.TypeOf(typeof(RedirectToActionResult)), "Wrong type");
            Assert.That(((RedirectToActionResult)result).ActionName.ToLower(), Is.EqualTo("getdetails"), "Wrong action");
        }

        [TestCase("12345")]
        public void EditActionTest(string personId)
        {
            var result = _controller.Edit(personId);
            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)), "Wrong type");
        }

        [TestCase("1", "Lam", "Tran", GenderType.Male, "23/2/2003", "0123456789", "Ha Noi", true)]
        public void EditPersonActionTest(
            string id,
            string firstName,
            string lastName,
            GenderType gender,
            string birthday,
            string phoneNumber,
            string birthPlace,
            bool isGraduated
            )
        {
            var fakePerson = new RequestPerson
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Birthday = DateOnly.Parse(birthday),
                PhoneNumber = phoneNumber,
                BirthPlace = birthPlace,
                IsGraduated = isGraduated
            };
            var result = _controller.EditPerson(fakePerson);
            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)), "Wrong type");
        }

        [TestCase("12345")]
        public void DeleteActionTest(string id)
        {
            var result = _controller.Delete(id);
            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)), "Wrong type");
        }

        [TestCase(true, "Lam")]
        public void DeleteConfirmActionTest(bool success, string name)
        {
            var result = _controller.DeleteConfirm(success, name);
            Assert.That(result, Is.TypeOf(typeof(ViewResult)), "Wrong type");
        }

        [TestCase("Lam")]
        public void GetDetailsActionTest(string id)
        {
            var result = _controller.GetDetails(id);
            Assert.That(result, Is.TypeOf(typeof(NotFoundResult)), "Wrong type");
        }
    }
}