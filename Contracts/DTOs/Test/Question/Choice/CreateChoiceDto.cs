
using System.ComponentModel.DataAnnotations;


namespace Contracts.DTOs.Test.Question.Choice
{
    public class CreateChoiceDto
    {
        [MaxLength(512)]
        public required string Title { get; set; }
        public int Score { get; set; }
        public int? Order { get; set; }
    }
}
