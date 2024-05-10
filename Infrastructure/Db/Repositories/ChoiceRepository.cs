using Domains.Entities;
using Domains.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;


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

        public async Task<int> CalculateScoreAsync(List<long> selectedChoices, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Choices
                .Join(selectedChoices,
                      ch => ch.Id,
                      selected => selected,
                      (ch, selected) => ch.Score)
                .SumAsync(cancellationToken);
        }

        public async Task<Choice?> FetchByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Choices.Where(t => t.Id == id).FirstOrDefaultAsync();
        }
    }
}
