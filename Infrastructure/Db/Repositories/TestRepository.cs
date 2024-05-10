using Contracts.DTOs.Test;
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
    public class TestRepository: ITestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TestRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task CreateAsync(Test test, CancellationToken cancellationToken = default)
        {
            _dbContext.Tests.Add(test);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Test?> FetchByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Tests.Where(t => t.Id == id)
                                         .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAsync(Test test, CreateTestDto dto, CancellationToken cancellationToken = default)
        {
            test.Description = dto.Description;
            test.Name = dto.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Test>> FetchTestListAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Tests.ToListAsync();
        }
    }
}
