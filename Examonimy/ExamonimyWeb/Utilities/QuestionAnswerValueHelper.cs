using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace ExamonimyWeb.Utilities
{
    public static class QuestionAnswerValueHelper
    {
        public static char GetAnswerValueFromByte(byte answer)
        {
            return answer switch
            {
                0 => 'A',
                1 => 'B',
                2 => 'C',
                3 => 'D',
                _ => throw new SwitchExpressionException(answer)
            };
        }

        public static byte GetAnswerValueFromChar(char answer)
        {
            return answer switch
            {
                'A' or 'a' => 0,
                'B' or 'b' => 1,
                'C' or 'c' => 2,
                'D' or 'd' => 3,
                _ => throw new SwitchExpressionException(answer)
            };
        }

        public static IEnumerable<char> GetAnswerValuesFromStringForChoices(string answer)
        {
            return answer.Split('|').Select(e => GetAnswerValueFromByte(byte.Parse(e))).ToImmutableSortedSet();
        }

        public static char GetAnswerValueFromBool(bool answer)
        {
            return answer ? 'Đ' : 'S';
        }

        public static bool GetAnswerValueFromCharForTrueFalse(char answer)
        {
            return answer switch
            {
                'Đ' or 'đ' => true,
                'S' or 's' => false,
                _ => throw new SwitchExpressionException(answer)
            };
        }

        public static string[] GetAnswerValuesFromStringForBlanks(string answers)
        {
            return answers.Split('|');          
        }

        public static string GetAnswerValuesFromListOfStringForBlanks(List<string> answers)
        {
            return string.Join('|', answers);
        }

        public static string GetAnswerValuesFromListOfChar(List<char> answers)
        {
            var temp = answers.Select(a => GetAnswerValueFromChar(a));
            return string.Join('|', temp);
        }
    }
}
