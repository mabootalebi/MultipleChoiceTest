using Domains.Entities;
using Domains.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Db.Repositories
{
    public class AnsweredTestRepository: IAnsweredTestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AnsweredTestRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task CreateAsync(AnsweredTest answer, CancellationToken cancellationToken = default)
        {
            _dbContext.AnsweredTests.Add(answer);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
