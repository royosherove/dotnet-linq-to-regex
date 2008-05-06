using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using FlimFlan.ReadableRex;
using LinqToAnything.Demos;

namespace Osherove.LinqToRegex
{
    public class RegexQuery:IEnumerable<Match>
    {
        private readonly string input;
        private object lastPatternRetVal;
        private RegexQuery(string input)
        {
            this.input = input;
        }
        public static RegexQuery Against(string input)
        {
            return new RegexQuery(input);
        }

        private string _regex;
        public RegexQuery Where(Expression<Func<Pattern,bool>> predicate)
        {
            _regex = new PatternVisitor().VisitExpression(predicate).ToString();
            return this;
        }

        public RegexQuery Select<T>(Expression<Func<Pattern,T>> selector)
        {
            return this;
        }
        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Match>) this).GetEnumerator();
        }

        #endregion

        #region IEnumerable<Match> Members

        public IEnumerator<Match> GetEnumerator()
        {
            MatchCollection matches = Regex.Matches(input, _regex);
            foreach (Match found in matches)
            {
                yield return found;
            }
        }

        #endregion
    }
}
