
namespace Contracts.DTOs.Test.Analysis
{
    public class CreateAnalysisDto
    {
        public int MinScore { get; set; }
        public int MaxScore { get; set; }

        public required string Description { get; set; }
    }
}
