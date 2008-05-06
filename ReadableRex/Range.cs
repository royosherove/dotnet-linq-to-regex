using System;

namespace FlimFlan.ReadableRex
{
    public class Range
    {
        public static Pattern Of(char from, char to)
        {
            return new Pattern(from + "-" + to);
        }
        public static Pattern Of(int from, int to)
        {
            return new Pattern(from + "-" + to);
        }
        public static Pattern AnyLetter
        {
            get
            {
                return new Pattern("a-zA-Z");
            }
        }
        public static Pattern AnyLowercaseLetter
        {
            get
            {
                return new Pattern("a-z");
            }
        }
        public static Pattern AnyUppercaseLetter
        {
            get
            {
                return new Pattern("A-Z");
            }
        }

    }
}
