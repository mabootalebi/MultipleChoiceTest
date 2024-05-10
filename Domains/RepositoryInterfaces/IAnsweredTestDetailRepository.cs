

using Domains.Entities;

namespace Domains.RepositoryInterfaces
{
    public interface IAnsweredTestDetailRepository
    {
        Task CreateRangeAsync(List<AnsweredTestDetail> details, CancellationToken cancellationToken = default);
    }
}
