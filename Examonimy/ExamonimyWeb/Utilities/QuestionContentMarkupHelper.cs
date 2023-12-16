using System.Text.RegularExpressions;

namespace ExamonimyWeb.Utilities
{
    public static class QuestionContentMarkupHelper
    {
        public static string GetFormattedFillInBlankQuestionContentMarkup(string rawMarkup)
        {
            var pattern = "__";
            var i = 0;
            return Regex.Replace(rawMarkup, pattern, m => $"<span class=\"mx-1 inline-flex h-6 w-6 flex-shrink-0 items-center justify-center rounded-full bg-gray-700\">\r\n    <span class=\"text-white font-semibold text-xs\">{++i}</span>\r\n</span>\r\n<span class=\"mr-2 inline-flex w-20 border-b-2 border-gray-300\"></span>");
        }
    }
}
