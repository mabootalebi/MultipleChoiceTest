using Domains.Entities;
using Domains.RepositoryInterfaces;
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
    }
}
