

namespace Contracts.DTOs.Authentication
{
    public class LoginResultDto
    {
        public required string Token {  get; set; }
        public DateTime Expiration {  get; set; }
    }
}
