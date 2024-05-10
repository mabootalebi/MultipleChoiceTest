using Domains.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db
{
    public sealed class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Test> Tests { get; set; }
        //public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        //public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Analysis> Analysis { get; set; }
        //public DbSet<AnsweredTest> AnsweredTests { get; set; }
        //public DbSet<AnsweredTestDetail> AnsweredTestDetails { get; set; }


        // remove it if there is no extra config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
