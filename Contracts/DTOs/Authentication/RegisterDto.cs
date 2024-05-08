

namespace Contracts.DTOs.Authentication
{
    public class RegisterDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string NationalCode { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
