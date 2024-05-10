using Domains.Entities;
using Domains.RepositoryInterfaces;


namespace Infrastructure.Db.Repositories
{
    public class ChoiceRepository: IChoiceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChoiceRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task CreateRangeAsync(List<Choice> choices, CancellationToken cancellationToken = default)
        {
            _dbContext.Choices.AddRange(choices);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
