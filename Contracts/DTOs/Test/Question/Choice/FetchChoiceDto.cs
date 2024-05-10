
namespace Contracts.DTOs.Test.Question.Choice
{
    public class FetchChoiceDto
    {
        public long Id { get; set; }
        public required string Title { get; set; }
        public int Score { get; set; }
        public int? Order { get; set; }
    }
}
