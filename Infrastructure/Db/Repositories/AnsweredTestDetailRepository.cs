using Domains.Entities;
using Domains.RepositoryInterfaces;

namespace Infrastructure.Db.Repositories
{
    public class AnsweredTestDetailRepository: IAnsweredTestDetailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AnsweredTestDetailRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task CreateRangeAsync(List<AnsweredTestDetail> details, CancellationToken cancellationToken = default)
        {
            _dbContext.AnsweredTestDetail.AddRange(details);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
