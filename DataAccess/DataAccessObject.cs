using Model;
using System;

namespace DataAccess
{
    public class DataAccessObject : IDataAccess
    {
        private static List<Person> _values;
        public DataAccessObject()
        {
            _values = [
                new Person() { FirstName = "Linh", LastName = "Nguyen", Birthday = new DateOnly(1995, 7, 15), Gender = GenderType.Female, BirthPlace = "Ho Chi Minh", IsGraduated = false, PhoneNumber = "0231231743" },
                new Person() { FirstName = "Huy", LastName = "Pham", Birthday = new DateOnly(1990, 1, 10), Gender = GenderType.Male, BirthPlace = "Da Nang", IsGraduated = true, PhoneNumber = "0231231744" },
                new Person() { FirstName = "Mai", LastName = "Le", Birthday = new DateOnly(1992, 12, 5), Gender = GenderType.Female, BirthPlace = "Can Tho", IsGraduated = false, PhoneNumber = "0231231745" },
                new Person() { FirstName = "Tuan", LastName = "Hoang", Birthday = new DateOnly(2000, 6, 30), Gender = GenderType.Male, BirthPlace = "Hai Phong", IsGraduated = true, PhoneNumber = "0231231746" },
                new Person() { FirstName = "Anh", LastName = "Vu", Birthday = new DateOnly(1991, 11, 20), Gender = GenderType.Female, BirthPlace = "Hue", IsGraduated = true, PhoneNumber = "0231231747" },
                new Person() { FirstName = "Bao", LastName = "Tran", Birthday = new DateOnly(1994, 4, 18), Gender = GenderType.Male, BirthPlace = "Nha Trang", IsGraduated = false, PhoneNumber = "0231231748" },
                new Person() { FirstName = "Chi", LastName = "Nguyen", Birthday = new DateOnly(1996, 8, 25), Gender = GenderType.Female, BirthPlace = "Quang Ninh", IsGraduated = true, PhoneNumber = "0231231749" },
                new Person() { FirstName = "Duc", LastName = "Pham", Birthday = new DateOnly(1989, 2, 14), Gender = GenderType.Male, BirthPlace = "Binh Dinh", IsGraduated = true, PhoneNumber = "0231231750" },
                new Person() { FirstName = "Em", LastName = "Le", Birthday = new DateOnly(1997, 5, 30), Gender = GenderType.Female, BirthPlace = "Vung Tau", IsGraduated = false, PhoneNumber = "0231231751" },
                new Person() { FirstName = "Phuc", LastName = "Hoang", Birthday = new DateOnly(1992, 9, 12), Gender = GenderType.Male, BirthPlace = "Dong Nai", IsGraduated = true, PhoneNumber = "0231231752" },
                new Person() { FirstName = "Giang", LastName = "Vu", Birthday = new DateOnly(1993, 3, 22), Gender = GenderType.Female, BirthPlace = "Ha Noi", IsGraduated = true, PhoneNumber = "0231231753" },
                new Person() { FirstName = "Hanh", LastName = "Tran", Birthday = new DateOnly(1995, 7, 15), Gender = GenderType.Male, BirthPlace = "Ho Chi Minh", IsGraduated = false, PhoneNumber = "0231231754" },
                new Person() { FirstName = "Khanh", LastName = "Nguyen", Birthday = new DateOnly(1990, 1, 10), Gender = GenderType.Female, BirthPlace = "Da Nang", IsGraduated = true, PhoneNumber = "0231231755" },
                new Person() { FirstName = "Loi", LastName = "Pham", Birthday = new DateOnly(2004, 12, 5), Gender = GenderType.Male, BirthPlace = "Can Tho", IsGraduated = false, PhoneNumber = "0231231756" },
                new Person() { FirstName = "Minh", LastName = "Le", Birthday = new DateOnly(1988, 6, 30), Gender = GenderType.Female, BirthPlace = "Hai Phong", IsGraduated = true, PhoneNumber = "0231231757" },
                new Person() { FirstName = "Nghia", LastName = "Hoang", Birthday = new DateOnly(1991, 11, 20), Gender = GenderType.Male, BirthPlace = "Hue", IsGraduated = true, PhoneNumber = "0231231758" },
                new Person() { FirstName = "Oanh", LastName = "Vu", Birthday = new DateOnly(2005, 4, 18), Gender = GenderType.Female, BirthPlace = "Nha Trang", IsGraduated = false, PhoneNumber = "0231231759" },
                new Person() { FirstName = "Phuong", LastName = "Tran", Birthday = new DateOnly(1996, 8, 25), Gender = GenderType.Male, BirthPlace = "Quang Ninh", IsGraduated = true, PhoneNumber = "0231231760" },
                new Person() { FirstName = "Quyen", LastName = "Nguyen", Birthday = new DateOnly(2000, 2, 14), Gender = GenderType.Female, BirthPlace = "Binh Dinh", IsGraduated = true, PhoneNumber = "0231231761" }
            ];
        }

        public IEnumerable<ResponsePerson> ListAll()
        {
            return _values.Select(p => p.ToResponsePerson());
        }

        public ResponsePerson Create(RequestPerson person)
        {
            var newPerson = new Person(person);
            _values.Add(newPerson);
            return newPerson.ToResponsePerson();
        }

        public ResponsePerson? Delete(string id)
        {
            var foundPerson = _values.Where(p => p.Id.ToString().Equals(id)).FirstOrDefault();
            if (foundPerson == null) return null;
            _values.Remove(foundPerson);
            return foundPerson.ToResponsePerson();
        }

        public ResponsePerson? Update(RequestPerson person)
        {
            var foundPerson = _values.Find(p => p.Id.ToString().Equals(person.Id));
            if (foundPerson == null) return null;
            foundPerson = new Person()
            {
                Id = foundPerson.Id,
                FirstName =person.FirstName,
                LastName =person.LastName,
                Gender = person.Gender,
                PhoneNumber = person.PhoneNumber,
                Birthday = person.Birthday,
                BirthPlace = person.BirthPlace,
                IsGraduated = person.IsGraduated,
            };
            Delete(person.Id);
            _values.Add(foundPerson);
            return foundPerson.ToResponsePerson();
        }

        public ResponsePerson? Get(string id)
        {
            var foundPerson = _values.Find(p => p.Id.ToString().Equals(id));
            if (foundPerson == null) return null;
            return foundPerson.ToResponsePerson();
        }
    }
}
