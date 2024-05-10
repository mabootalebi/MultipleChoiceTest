using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Entities
{
    public class Analysis: BaseEntity<int>
    {
        [ForeignKey("Test")]
        public int TestId {  get; set; }
        public required virtual Test Test { get; set; }

        //[ForeignKey("Category")]
        //public int? CategoryId {  get; set; }
        //public virtual Category? Category { get; set; }

        public int MinScore {  get; set; }
        public int MaxScore {  get; set; }

        public required string Description {  get; set; }
    }
}
