using Domains.Entities;


namespace Domains.RepositoryInterfaces
{
    public interface IAnalysisRepository
    {
        Task CreateAsync(Analysis analysis, CancellationToken cancellationToken = default);
    }
}
