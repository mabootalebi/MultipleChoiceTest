

using Contracts.DTOs.Test.Question;

namespace Contracts.DTOs.Test
{
    public class FetchTestDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        public List<FetchQuestionDto>? Questions { get; set; }
    }
}
