using Contracts.DTOs.Test.Question.Choice;


namespace Contracts.DTOs.Test.Question
{
    public class FetchQuestionDto
    {
        public long Id { get; set; }
        public required string Description { get; set; }
        public int? Order { get; set; }

        public List<FetchChoiceDto>? Choices { get; set; }
    }
}
