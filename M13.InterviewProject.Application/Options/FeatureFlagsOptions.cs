namespace M13.InterviewProject.Application.Options;

public class FeatureFlagsOptions
{
    public static string Section { get; } = "FeatureFlags";
    public bool UseRedis { get; set; }
}