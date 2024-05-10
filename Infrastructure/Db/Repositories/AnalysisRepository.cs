using Domains.Entities;
using Domains.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Db.Repositories
{
    public class AnalysisRepository: IAnalysisRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AnalysisRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task CreateAsync(Analysis analysis, CancellationToken cancellationToken = default)
        {
            _dbContext.Analysis.AddRange(analysis);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Analysis?> FetchAnalysisBasedOnScoreAsync(int testId, int score, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Analysis
                .Where(t => t.TestId == testId && 
                            t.MinScore<= score && 
                            t.MaxScore>= score)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
