using Contracts.DTOs.Test.Question.Choice;
using System.ComponentModel.DataAnnotations;


namespace Contracts.DTOs.Test.Question
{
    public class CreateQuestionDto
    {
        [MaxLength(2048)]
        public required string Description { get; set; }
        public int? Order { get; set; }

        public List<CreateChoiceDto>? Choices { get; set; }
    }
}
