
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Entities
{
    public class Question: BaseEntity<long>
    {
        [ForeignKey("Test")]
        public int TestId { get; set; }
        public virtual required Test Test { get; set; }

        [MaxLength(2048)]
        public required string Description { get; set; }
        public int? Order { get; set; }

        //public virtual ICollection<QuestionCategory>? QuestionCategories { get; set; }
        public virtual ICollection<Choice>? Choices { get; set; }
        //public virtual ICollection<AnsweredTestDetail>? AnsweredTestDetails { get; set; }

    }
}
