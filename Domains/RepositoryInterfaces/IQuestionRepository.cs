using Contracts.DTOs.Test.Question;
using Domains.Entities;


namespace Domains.RepositoryInterfaces
{
    public interface IQuestionRepository
    {
        Task CreateAsync(Question question, CancellationToken cancellationToken = default);
        Task<Question?> FindById(long id, CancellationToken cancellationToken = default);
        Task<List<Question>?> FetchAllQuestionsByTestId(int testId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Question question, CreateQuestionDto dto, CancellationToken cancellationToken = default);
    }
}
