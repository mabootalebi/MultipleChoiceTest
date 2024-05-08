
namespace Contracts.DTOs
{
    public class ResultDto
    {
        public Status? Status { get; set; }

    }

    public class ResultDto<T> : ResultDto where T : class
    {
        public T? Parameter { get; set; }
    }
}
