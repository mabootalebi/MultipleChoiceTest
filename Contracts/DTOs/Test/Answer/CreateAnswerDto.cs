

namespace Contracts.DTOs.Test.Answer
{
    public class CreateAnswerDto
    {
        public int TestId {  get; set; }
        public List<long>? ChoiceIds {  get; set; }
    }
}
