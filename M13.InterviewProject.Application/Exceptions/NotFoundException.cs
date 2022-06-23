namespace M13.InterviewProject.Application.Exceptions;

public class NotFoundException: Exception
{
    public NotFoundException(string? message) : base(message)
    {
    }
}