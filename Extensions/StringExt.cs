using System.Text.RegularExpressions;

namespace WayFinder.Extensions;

public static class StringExt
{
    public static string ConvertToLineBreaks(this string inputString) {
        Regex regEx = new Regex(@"(\\n|\\r)+");
        return regEx.Replace(inputString, "");
    }
}
