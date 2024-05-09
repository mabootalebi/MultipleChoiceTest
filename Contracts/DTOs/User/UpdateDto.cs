using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs.User
{
    public class UpdateDto
    {
        [MaxLength(128)]
        public string? FirstName { get; set; }
        [MaxLength(128)]
        public string? LastName { get; set; }
        [MaxLength(128)]
        public string? FatherName { get; set; }
        [MaxLength(16)]
        public string? NationalCode { get; set; }
        [MaxLength(1024)]
        public string? Address { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber {  get; set; }
    }
}
