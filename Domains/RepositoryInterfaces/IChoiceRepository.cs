using Domains.Entities;


namespace Domains.RepositoryInterfaces
{
    public interface IChoiceRepository
    {
        Task CreateRangeAsync(List<Choice> choices, CancellationToken cancellationToken = default);
        Task<int> CalculateScoreAsync(List<long> selectedChoices, CancellationToken cancellationToken = default);
        Task<Choice?> FetchByIdAsync(long id, CancellationToken cancellationToken = default);
    }
}
