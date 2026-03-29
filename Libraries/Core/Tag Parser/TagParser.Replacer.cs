using System;
using System.Collections.Generic;
using System.Linq;



namespace Rune
{
    public partial class TagParser
    {
        public class Replacer
        {
            public string Replace(string input)
            {
                return string.Format(_nullableFuncReplacement.Invoke(), input);
            }



            public Func<string> Replacement
            {
                get => _nullableFuncReplacement.Func;
                set => _nullableFuncReplacement.Func = value;
            }



            public string Tag { get; set; } = string.Empty;



            private readonly NullableFunc<string> _nullableFuncReplacement = new(@"{0}");
        }



        public static Lazy<Dictionary<string, Replacer>> _replacers = new(() => new List<List<Replacer>>
        {
            _replacersColor.Value,
        }
        .SelectMany(r => r)
        .ToDictionary(r => r.Tag, r => r));
    }
}