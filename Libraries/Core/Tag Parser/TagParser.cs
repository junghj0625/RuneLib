using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;



namespace Rune
{
    public static partial class TagParser
    {
        public static string Parse(string text, Dictionary<string, Replacer> replacers)
        {
            string pattern = @"<(?<tag>[^>/\s]+)(?:\s*\/>|>(?<content>.*?)<\/\k<tag>>)";

            string result = Regex.Replace(text, pattern, match =>
            {
                string tag = match.Groups["tag"].Value;

                if (!replacers.TryGetValue(tag, out var replacer)) return match.Value;

                string content = match.Groups["content"].Success ? match.Groups["content"].Value : string.Empty;

                return replacer.Replace(content);
            });

            return result;
        }

        public static string Parse(string text, List<Replacer> replacers)
        {
            return Parse(text, replacers.ToDictionary(r => r.Tag, r => r));
        }

        public static string Parse(string text, params Replacer[] replacers)
        {
            return Parse(text, new List<Replacer>(replacers));
        }

        public static string Parse(string text)
        {
            return Parse(text, _replacers.Value);
        }

        public static string Tag(string text, string tag)
        {
            if (string.IsNullOrEmpty(text)) return text;

            return $"<{tag}>{text}</{tag}>";
        }

        public static string Color(string text, ColorPalette.ColorType color)
        {
            if (string.IsNullOrEmpty(text)) return text;

            return $"<color={ColorPalette.GetColorCode(color)}>{text}</color>";
        }
    }
}