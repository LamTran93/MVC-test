using DataAccess;
using ClosedXML.Excel;
using Model;

namespace Business
{
    public class PersonService : IPersonService
    {
        private readonly IDataAccess _data;
        public PersonService(IDataAccess data)
        {
            _data = data;
        }
        public IEnumerable<ResponsePerson> GetAll()
        {
            return _data.ListAll();
        }
        public IEnumerable<ResponsePerson> GetAllMales()
        {
            return _data.ListAll().Where(p => p.Gender == GenderType.Male);
        }
        public ResponsePerson? GetOldest()
        {
            return _data.ListAll().MinBy(p => p.Birthday);
        }
        public IEnumerable<ResponsePerson> GetPersonsByYear(int year, AgeComparer comparer = AgeComparer.Equal)
        {
            var persons = _data.ListAll();
            switch (comparer)
            {
                case AgeComparer.Equal:
                    return persons.Where(p => p.Birthday.Year == year);
                case AgeComparer.Higher:
                    return persons.Where(p => p.Birthday.Year > year);
                case AgeComparer.Lower:
                    return persons.Where(p => p.Birthday.Year < year);
                default:
                    return [];
            }
        }
        public Stream ToExcel()
        {
            using var workbook = new XLWorkbook();
            var stream = new MemoryStream();

            var worksheet = workbook.Worksheets.Add("Persons");

            worksheet.Cell(2, 2).Value = "#";
            worksheet.Cell(2, 3).Value = "First Name";
            worksheet.Cell(2, 4).Value = "Last Name";
            worksheet.Cell(2, 5).Value = "Gender";
            worksheet.Cell(2, 6).Value = "Date Of Birth";
            worksheet.Cell(2, 7).Value = "Phone Number";
            worksheet.Cell(2, 8).Value = "Birth Place";
            worksheet.Cell(2, 9).Value = "Is Graduated";

            var count = 1;
            foreach (var i in _data.ListAll())
            {
                worksheet.Cell(count + 2, 2).Value = count;
                worksheet.Cell(count + 2, 3).Value = i.FirstName;
                worksheet.Cell(count + 2, 4).Value = i.LastName;
                worksheet.Cell(count + 2, 5).Value = i.Gender.ToString();
                worksheet.Cell(count + 2, 6).Value = $"{i.Birthday}";
                worksheet.Cell(count + 2, 7).Value = i.PhoneNumber;
                worksheet.Cell(count + 2, 8).Value = i.BirthPlace;
                worksheet.Cell(count + 2, 9).Value = i.IsGraduated ? "Yes" : "No";
                count++;
            }

            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;

        }

        public ResponsePerson Create(RequestPerson person)
        {
            return _data.Create(person);
        }

        public ResponsePerson? Update(RequestPerson person)
        {
            return _data.Update(person);
        }

        public ResponsePerson? Delete(string id)
        {
            return _data.Delete(id);
        }

        public ResponsePerson? Get(string id)
        {
            return _data.Get(id);
        }
    }
}
