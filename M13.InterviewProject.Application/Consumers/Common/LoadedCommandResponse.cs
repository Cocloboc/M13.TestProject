namespace M13.InterviewProject.Application.Common;

public record LoadedCommandResponse<T>: CommandResponse
{
    public T Data { get; init; }
}