using Model;

public class Person
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public DateOnly Birthday { get; set; }
    public string PhoneNumber { get; set; }
    public string BirthPlace { get; set; }
    public bool IsGraduated { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }

    public Person()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        LastUpdatedAt = DateTime.Now;
    }

    public Person(RequestPerson person)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        LastUpdatedAt = DateTime.Now;
        FirstName = person.FirstName;
        LastName = person.LastName;
        Gender = person.Gender;
        Birthday = person.Birthday;
        PhoneNumber = person.PhoneNumber;
        BirthPlace = person.BirthPlace;
        IsGraduated = person.IsGraduated;
    }

    public ResponsePerson ToResponsePerson()
    {
        return new ResponsePerson
        {
            Id = Id.ToString(),
            FirstName = FirstName,
            LastName = LastName,
            Gender = Gender,
            IsGraduated = IsGraduated,
            Birthday = Birthday,
            PhoneNumber = PhoneNumber,
            BirthPlace = BirthPlace
        };
    }
}

