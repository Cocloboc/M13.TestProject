namespace M13.InterviewProject.Application.Consumers.SpellChecker.Common;

public class SpellCheckError
{
    public string Word { get; set; }
    public List<string> PossibleOptions { get; set; }
}