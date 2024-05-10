using System.ComponentModel.DataAnnotations.Schema;


namespace Domains.Entities
{
    public class AnsweredTestDetail : BaseEntity<long>
    {
        [ForeignKey("AnsweredTest")]
        public long AnsweredTestId { get; set; }
        public required virtual AnsweredTest AnsweredTest { get; set; }

        [ForeignKey("Choice")]
        public long ChoiceId { get; set; }
        public required virtual Choice Choice { get; set; }
    }
}
