using Contracts.DTOs;
using Contracts.DTOs.Test;
using Domains.Entities;
using Domains.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;


namespace Services
{
    public class TestCreatorServices : ITestCreatorServices
    {
        private readonly ILogger<TestCreatorServices> _logger;
        private readonly ITestRepository _testRepository;

        public TestCreatorServices(ILogger<TestCreatorServices> logger, ITestRepository testRepository)
        {
            _logger = logger;
            _testRepository = testRepository;
        }

        public async Task<ResultDto<FetchTestDto>> CreateTestAsync(CreateTestDto dto, CancellationToken cancellationToken = default)
        {
            var test = new Test
            {
                Name = dto.Name,
                Description = dto.Description
            };
            await _testRepository.CreateAsync(test, cancellationToken);
            return new ResultDto<FetchTestDto>
            {
                Status = CustomStatuses.Success,
                Parameter = MakeFetchTestDtoInstance(test)
            };
        }

        private FetchTestDto MakeFetchTestDtoInstance(Test test) =>
            new FetchTestDto
            {
                Name = test.Name,
                Description = test.Description,
                Id = test.Id
            };

        public async Task<ResultDto<FetchTestDto>> UpdateTestAsync(int id, CreateTestDto dto, CancellationToken cancellationToken = default)
        {
            var test = await _testRepository.FetchByIdAsync(id);
            if (test is null)
                return new ResultDto<FetchTestDto>
                {
                    Status = CustomStatuses.TestNotFound
                };

            await _testRepository.UpdateAsync(test, dto, cancellationToken);

            return new ResultDto<FetchTestDto>
            {
                Status = CustomStatuses.Success,
                Parameter = MakeFetchTestDtoInstance(test)
            };
        }

        public async Task<ResultDto<List<FetchTestDto>>> FetchTestListAsync(CancellationToken cancellationToken = default)
        {
            var list = (await _testRepository.FetchTestListAsync())
                .Select(t => new FetchTestDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description
                }).ToList();

            return new ResultDto<List<FetchTestDto>>
            {
                Status = CustomStatuses.Success,
                Parameter = list
            };
        }
    }
}
