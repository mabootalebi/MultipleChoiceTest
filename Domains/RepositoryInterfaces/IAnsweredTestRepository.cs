

using Domains.Entities;

namespace Domains.RepositoryInterfaces
{
    public interface IAnsweredTestRepository
    {
        Task CreateAsync(AnsweredTest answer, CancellationToken cancellationToken = default);
    }
}
