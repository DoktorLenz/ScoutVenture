using ScoutVenture.CoreContracts.User;

namespace ScoutVenture.Models
{
    public class PersonalDataDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public PersonalData ToDomainObject()
        {
            return new PersonalData
            {
                FirstName = FirstName,
                LastName = LastName
            };
        }
    }
}