
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class Test: BaseEntity<int>
    {
        [MaxLength(128)]
        public required string Name {  get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }

        public virtual ICollection<Question>? Questions { get; set; }
        public virtual ICollection<Analysis>? Analysis { get; set; }
        public virtual ICollection<AnsweredTest>? AnsweredTests { get; set; }
    }
}
