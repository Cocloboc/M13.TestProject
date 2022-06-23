namespace M13.InterviewProject.Application.Common;

public record QueryResponse<T>
{
    public T Data { get; init; }
}