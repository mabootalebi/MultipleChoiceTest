using Contracts.DTOs.Test;
using Domains.Entities;


namespace Domains.RepositoryInterfaces
{
    public interface ITestRepository
    {
        Task CreateAsync(Test test, CancellationToken cancellationToken = default);
        Task<Test?> FetchByIdAsync(int id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Test test, CreateTestDto dto, CancellationToken cancellationToken = default);
        Task<List<Test>> FetchTestListAsync(CancellationToken cancellationToken = default);
    }
}
