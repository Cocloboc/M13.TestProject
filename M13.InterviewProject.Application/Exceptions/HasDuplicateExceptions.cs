namespace M13.InterviewProject.Application.Exceptions;

public class HasDuplicateExceptions: Exception
{
    public HasDuplicateExceptions(string? message) : base(message)
    {
    }
}