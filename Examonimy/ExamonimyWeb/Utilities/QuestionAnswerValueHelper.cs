using System.Collections.Immutable;

namespace ExamonimyWeb.Utilities
{
    public static class QuestionAnswerValueHelper
    {
        public static char GetAnswerValueFromOneCorrectAnswer(byte correctAnswer)
        {
            return correctAnswer switch
            {
                0 => 'A',
                1 => 'B',
                2 => 'C',
                3 => 'D',
                _ => throw new ArgumentException(null, nameof(correctAnswer))
            };
        }

        public static IEnumerable<char> GetAnswerValuesFromMultipleCorrectAnswers(string correctAnswers)
        {
            return correctAnswers.Split('|').Select(e => GetAnswerValueFromOneCorrectAnswer(byte.Parse(e))).ToImmutableSortedSet();
        }

        public static char GetAnswerValueFromTrueFalse(bool correctAnswer)
        {
            return correctAnswer ? 'Đ' : 'S';
        }

        public static string[] GetAnswerValuesFromFillInBlankCorrectAnswers(string correctAnswers)
        {
            return correctAnswers.Split('|');          
        }
    }
}
