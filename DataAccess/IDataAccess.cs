using Model;

namespace DataAccess
{
    public interface IDataAccess
    {
        public IEnumerable<ResponsePerson> ListAll();
        public ResponsePerson? Get(string id);
        public ResponsePerson Create(RequestPerson person);
        public ResponsePerson? Delete(string id);
        public ResponsePerson? Update(RequestPerson person);
    }
}
