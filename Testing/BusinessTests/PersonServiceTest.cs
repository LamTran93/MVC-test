using Business;
using DataAccess;
using Model;

namespace Testing.BusinessTests
{
    public class PersonServiceTest
    {
        private PersonService _service;

        [SetUp]
        public void SetUp()
        {
            var dao = new DataAccessObject();
            _service = new PersonService(dao);
        }

        [Test]
        public void GetAllTest()
        {
            var result = _service.GetAll();
            Assert.IsInstanceOf<IEnumerable<ResponsePerson>>(result);
        }

        [Test]
        public void GetAllMalesTest()
        {
            var result = _service.GetAllMales();
            Assert.IsInstanceOf<IEnumerable<ResponsePerson>>(result);
            foreach (var person in result)
            {
                Assert.That(person.Gender, Is.EqualTo(GenderType.Male));
            }
        }

        [Test]
        public void GetOldestTest()
        {
            var list = _service.GetAll();
            var result = _service.GetOldest();
            Assert.That(result, Is.Not.Null);
            foreach (var person in list)
            {
                Assert.That(person.Birthday, Is.AtLeast(result.Birthday));
            }
        }

        [TestCase(2000, AgeComparer.Higher)]
        [TestCase(2000, AgeComparer.Equal)]
        [TestCase(2000, AgeComparer.Lower)]
        public void GetPersonsByYearTest(int year, AgeComparer comparer)
        {
            var result = _service.GetPersonsByYear(year, comparer);
            Assert.IsInstanceOf<IEnumerable<ResponsePerson>>(result);
        }

        [Test]
        public void ToExcelTest()
        {
            var result = _service.ToExcel();
            Assert.IsInstanceOf<Stream>(result);
        }

        [TestCase("Lam", "Tran", GenderType.Male, "23/2/2003", "0123456789", "Ha Noi", true)]
        public void CreateTest(
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
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                Birthday = DateOnly.Parse(birthday),
                PhoneNumber = phoneNumber,
                BirthPlace = birthPlace,
                IsGraduated = isGraduated
            };
            var result = _service.Create(fakePerson);
            Assert.That(result, Is.TypeOf(typeof(ResponsePerson)));
        }

        [TestCase("Lam", "Tran", GenderType.Male, "23/2/2003", "0123456789", "Ha Noi", true)]
        public void UpdateTest(
            string firstName,
            string lastName,
            GenderType gender,
            string birthday,
            string phoneNumber,
            string birthPlace,
            bool isGraduated
            )
        {
            var id = _service.GetAll().First().Id;
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
            var result = _service.Update(fakePerson);
            Assert.That(result, Is.TypeOf(typeof(ResponsePerson)));
        }

        [Test]
        public void DeleteTest()
        {
            var id = _service.GetAll().First().Id;
            var result = _service.Delete(id);
            Assert.That(result, Is.TypeOf(typeof(ResponsePerson)));
        }

        [Test]
        public void GetTest()
        {
            var id = _service.GetAll().First().Id;
            var result = _service.Get(id);
            Assert.That(result, Is.TypeOf(typeof(ResponsePerson)));
        }
    }
}
