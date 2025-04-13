using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class ResponsePerson
    {
        public string Id { get; set; }
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        [DisplayName("Date of Birth")]
        public DateOnly Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }
        [DisplayName("Graduated")]
        public bool IsGraduated { get; set; }

        public ResponsePerson() { }
    }
}
