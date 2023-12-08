namespace ExamonimyWeb.Extensions
{
    public static class StringExtensions
    {
        public static string TrimHtmlMarkupForQuestionContent(this string markup)
        {
            var temp = markup.IndexOf("</");
            var result = markup.Substring(0, temp);
            return result.Substring(0, result.Length < 100 ? result.Length - 1 : 100 - 1);          
        }
    }
}
