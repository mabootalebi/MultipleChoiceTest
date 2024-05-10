using Contracts.DTOs.Test;
using Contracts.DTOs.Test.Question;
using Domains.Entities;
using Domains.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Db.Repositories
{
    public class QuestionRepository: IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task CreateAsync(Question question, CancellationToken cancellationToken = default)
        {
            _dbContext.Questions.Add(question);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Question?> FindById(long id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Questions.Where(t => t.Id == id)
                                             .Include(t => t.Choices)
                                             .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Question>?> FetchAllQuestionsByTestId(int testId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Questions.Where(t => t.TestId == testId)
                                             .Include(t => t.Choices)
                                             .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Question question, CreateQuestionDto dto, CancellationToken cancellationToken = default)
        {
            question.Description = dto.Description;
            question.Order = dto.Order;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
