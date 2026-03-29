// Title: Utils.Text.cs
// Version: 1.0.0
// Date: 2025-06-29



using System;
using System.Text.RegularExpressions;



namespace Rune.Utils
{
    public readonly struct Text
    {
        public static string GetTaggedText(string tag, string text)
        {
            return string.Format("<{0}>{1}</{0}>", tag, text);
        }

        public static Tuple<string, string> ParseTaggedText(string input)
        {
            string pattern = @"<([^>]+)>(.*?)</\1>";

            Regex regex = new(pattern);
            Match match = regex.Match(input);

            if (match.Success && match.Groups.Count >= 3)
            {
                string tag = match.Groups[1].Value;
                string content = match.Groups[2].Value;

                return new Tuple<string, string>(tag, content);
            }

            return null;
        }
    }
}