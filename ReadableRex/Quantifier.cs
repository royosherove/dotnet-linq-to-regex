using System;

namespace FlimFlan.ReadableRex
{
    public class Quantifier
    {
        Pattern _quantifiedExpression;

        internal Quantifier(Pattern quantifiedExpression)
        {
            _quantifiedExpression = quantifiedExpression;
        }

        public virtual Pattern Exactly(int timesToRepeat)
        {
            _quantifiedExpression.RegEx("{" + timesToRepeat + "}");
            return _quantifiedExpression;
        }

        public virtual Pattern ZeroOrMore
        {
            get
            {
                _quantifiedExpression.RegEx("*");
                return _quantifiedExpression;
            }
        }

        public virtual Pattern OneOrMore
        {
            get
            {
                _quantifiedExpression.RegEx("+");
                return _quantifiedExpression;
            }
        }

        public virtual Pattern Optional
        {
            get
            {
                _quantifiedExpression.RegEx("?");
                return _quantifiedExpression;
            }
        }

        public virtual Pattern AtLeast(int timesToRepeat)
        {
            _quantifiedExpression.RegEx("{" + timesToRepeat + ",}");
            return _quantifiedExpression;
        }

        public virtual Pattern AtMost(int timesToRepeat)
        {
            _quantifiedExpression.RegEx("{," + timesToRepeat + "}");
            return _quantifiedExpression;
        }

        public virtual Pattern InRange(int minimum, int maximum)
        {
            _quantifiedExpression.RegEx("{" + minimum + "," + maximum + "}");
            return _quantifiedExpression;
        }

        public Quantifier Lazy
        {
            get
            {
                return new LazyQuantifier(_quantifiedExpression);
            }
        }
    }
}
