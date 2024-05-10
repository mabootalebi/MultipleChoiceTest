
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Entities
{
    public class Choice: BaseEntity<long>
    {
        [MaxLength(512)]
        public required string Title { get; set; }
        
        [ForeignKey("Question")]
        public long QuestionId { get; set; }
        public required virtual Question Question { get; set; }
        
        public int Score { get; set; }
        public int? Order {  get; set; }

        //public virtual ICollection<AnsweredTestDetail>? AnsweredTestDetails { get; set; }
    }
}
