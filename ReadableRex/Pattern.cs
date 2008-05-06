using System;
using System.Text.RegularExpressions;
using System.Text;

namespace FlimFlan.ReadableRex
{
    public class Pattern
    {
        StringBuilder _content;

        /// <summary>
        /// Indicates creation of a new pattern
        /// </summary>
        public static Pattern With
        {
            get
            {
                return new Pattern(String.Empty);
            }
        }
    

        public Pattern(string content)
        {
            //Do we even need this public? Should we force everyone to use Pattern.With syntax?
            if (content == null) throw new ArgumentNullException("content");
            _content = new StringBuilder(content);
        }

        private string Eval()
        {
            return _content.ToString();
        }

        /// <summary>
        /// A string that will be properly escaped so that reserved characters are treated as literals
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Pattern Literal(string content)
        {
            string reservedCharacters = @".$^{[(|)*+?\";
            foreach (char character in content)
            {
                if (reservedCharacters.IndexOf(character) < 0)
                {
                    _content.Append(character);
                }
                else
                {
                    _content.Append('\\').Append(character);
                }
            }
            return this;
        }

        /// <summary>
        /// Any existing regular expression pattern.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public Pattern RegEx(string pattern)
        {
            _content.Append(pattern);
            return this;
        }

        public Pattern Anything
        {
            get
            {
                _content.Append(@".");
                return this;
            }
        }

        public Pattern Word
        {
            get
            {
                _content.Append(@"\w");
                return this;
            }
        }

        public Pattern Digit
        {
            get
            {
                _content.Append(@"\d");
                return this;
            }
        }

        public Pattern WhiteSpace
        {
            get
            {
                _content.Append(@"\s");
                return this;
            }
        }

        public override string ToString()
        {
            return Eval();
        }


        public Pattern NonWord
        {
            get
            {
                _content.Append(@"\W");
                return this;
            }
        }

        public Pattern NonDigit
        {
            get
            {
                _content.Append(@"\D");
                return this;
            }
        }

        public Pattern NonWhiteSpace
        {
            get
            {
                _content.Append(@"\S");
                return this;
            }
        }

        /// <summary>
        /// A subset of the pattern that can be referenced as ordinal captures
        /// </summary>
        /// <param name="innerExpression"></param>
        /// <returns></returns>
        public Pattern Group(Pattern innerExpression)
        {
            _content.AppendFormat("({0})", innerExpression.ToString());
            return this;
        }

        /// <summary>
        /// A subset of the pattern that can be referenced as a named capture
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="innerExpression"></param>
        /// <returns></returns>
        public Pattern NamedGroup(string groupName, Pattern innerExpression)
        {
            _content.AppendFormat("(?<{1}>{0})", innerExpression.ToString(), groupName);
            return this;
        }

        /// <summary>
        /// A non-capturing group
        /// </summary>
        /// <param name="innerExpression"></param>
        /// <returns></returns>
        public Pattern Phrase(Pattern innerExpression)
        {
            _content.AppendFormat("(?:{0})", innerExpression.ToString());
            return this;
        }

        /// <summary>
        /// Matches any single character contained within
        /// </summary>
        /// <param name="innerExpression"></param>
        /// <returns></returns>
        public Pattern Set(Pattern innerExpression)
        {
            _content.AppendFormat("[{0}]", innerExpression.ToString());
            return this;
        }

        /// <summary>
        /// Matches any single character not contained within
        /// </summary>
        /// <param name="innerExpression"></param>
        /// <returns></returns>
        public Pattern NegatedSet(Pattern innerExpression)
        {
            _content.AppendFormat("[^{0}]", innerExpression.ToString());
            return this;
        }

        /// <summary>
        /// Quantifies how many times to detect the previous element
        /// </summary>
        public Quantifier Repeat
        {
            get
            {
                return new Quantifier(this);
            }
        }

        public bool IsTrue()
        {
            return true;
        }

        /// <summary>
        /// Specifies that the match must occur at the beginning of the string or the beginning of the line
        /// </summary>
        public Pattern AtBeginning
        {
            get
            {
                _content.Append('^');
                return this;
            }
        }

        /// <summary>
        /// Specifies that the match must occur at the end of the string, before \n at the end of the string, or at the end of the line.
        /// </summary>
        public Pattern AtEnd
        {
            get
            {
                _content.Append('$');
                return this;
            }
        }

        public static implicit operator string(Pattern expression)
        {
            return expression.ToString();
        }

        public static Pattern operator +(Pattern first, Pattern second)
        {
            first._content.Append(second.ToString());
            return first;
        }

        public Alternation Choice
        {
            get
            {
                return new Alternation(this);
            }
        }
    }
}
