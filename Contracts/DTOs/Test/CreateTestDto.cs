using System.ComponentModel.DataAnnotations;

namespace Contracts.DTOs.Test
{
    public class CreateTestDto
    {
        [MaxLength(128)]
        public required string Name { get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }
    }
}
