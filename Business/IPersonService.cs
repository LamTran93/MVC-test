using Model;

namespace Business
{
    public interface IPersonService
    {
        public IEnumerable<ResponsePerson> GetAllMales();
        public ResponsePerson? GetOldest();
        public IEnumerable<ResponsePerson> GetAll();
        public Stream ToExcel();
        public IEnumerable<ResponsePerson> GetPersonsByYear(int year, AgeComparer comparer = AgeComparer.Equal);
        public ResponsePerson Create(RequestPerson person);
        public ResponsePerson? Update(RequestPerson person);
        public ResponsePerson? Delete(string id);
        public ResponsePerson? Get(string id);
    }
}
