using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Domains.Entities
{
    public class User: IdentityUser
    {
        [MaxLength(128)]
        public string? FirstName {  get; set; }
        [MaxLength(128)]
        public string? LastName {  get; set; }
        [MaxLength(128)]
        public string? FatherName {  get; set; }
        [MaxLength(16)]
        public string? NationalCode {  get; set; }
        [MaxLength(1024)]
        public string? Address {  get; set; }

        //public virtual ICollection<AnsweredTest>? AnsweredTests { get; set; }
    }
}
