using Domains.Entities;


namespace Domains.RepositoryInterfaces
{
    public interface IChoiceRepository
    {
        Task CreateRangeAsync(List<Choice> choices, CancellationToken cancellationToken = default);
    }
}
