using System;

namespace FlimFlan.ReadableRex
{
    public class Alternation
    {
        Pattern _precedingPattern;
        internal Alternation(Pattern precedingPattern)
        {
            _precedingPattern = precedingPattern;
        }

        public Pattern Either(Pattern firstOption, Pattern secondOption)
        {
            return _precedingPattern.RegEx( String.Format("({0}|{1})", firstOption, secondOption) );
        }

        public Pattern If(Pattern matched, Pattern then, Pattern otherwise)
        {
            return _precedingPattern.RegEx(String.Format("(?(?={0}){1}|{2})", matched, then, otherwise));
        }

        public Pattern If(string namedGroupToMatch, Pattern then, Pattern otherwise)
        {
            return _precedingPattern.RegEx(String.Format("(?({0}){1}|{2})", namedGroupToMatch, then, otherwise));
        }

        public Pattern If(int unnamedCaptureToMatch, Pattern then, Pattern otherwise)
        {
            return _precedingPattern.RegEx(String.Format("(?({0}){1}|{2})", unnamedCaptureToMatch, then, otherwise));
        }
    }
}
