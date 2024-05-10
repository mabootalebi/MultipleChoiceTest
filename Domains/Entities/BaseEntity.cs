using System.ComponentModel.DataAnnotations;

namespace Domains.Entities
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
