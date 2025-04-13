using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class RequestPerson
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        public DateOnly Birthday { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(120)]
        public string BirthPlace { get; set; }

        public bool IsGraduated { get; set; }

        public RequestPerson() { }

        public RequestPerson(ResponsePerson responsePerson)
        {
            Id = responsePerson.Id;
            FirstName = responsePerson.FirstName;
            LastName = responsePerson.LastName;
            Gender = responsePerson.Gender;
            Birthday = responsePerson.Birthday;
            PhoneNumber = responsePerson.PhoneNumber;
            BirthPlace = responsePerson.BirthPlace;
            IsGraduated = responsePerson.IsGraduated;
        }
    }
}
