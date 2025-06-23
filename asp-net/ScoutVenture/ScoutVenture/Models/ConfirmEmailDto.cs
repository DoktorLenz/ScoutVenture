namespace ScoutVenture.Models
{
    public class ConfirmEmailDto
    {
        public required string UserId { get; set; }
        public required string Code { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
    }
}