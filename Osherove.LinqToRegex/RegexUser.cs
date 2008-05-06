using System;
using FlimFlan.ReadableRex;
using Osherove.LinqToRegex;

namespace LinqToAnything.Demos
{
    public class RegexUser
    {
        void UseRegexQuery()
        {
            var query = from match in RegexQuery.Against("sdlfjsfl43r3490r98*(*Email@somewhere.com_dakj3j")
                        where match.Word.Repeat.AtLeast(1)
                                .Literal("@")
                                .Word.Repeat.AtLeast(1)
                                .Literal(".")
                                .Choice.Either(Pattern.With.Literal("com"),Pattern.With.Literal("net"))
                                .IsTrue()
                        select match;
            foreach (var match in query)
            {
                Console.WriteLine("found email: {0} at index {1}", match, match.Index);
            }
        }
    }
}
