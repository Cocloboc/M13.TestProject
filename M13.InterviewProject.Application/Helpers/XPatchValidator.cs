using System.Xml.XPath;

namespace M13.InterviewProject.Application.Helpers;

public static class XPatchValidator
{
    public static bool IsXPatchValid(this string xPath)
    {
        try
        {
            XPathExpression.Compile(xPath);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}