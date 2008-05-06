using System;

namespace FlimFlan.ReadableRex
{
    public class LazyQuantifier : Quantifier
    {
        public LazyQuantifier(Pattern quantifiedExpression) : base(quantifiedExpression) {}

        public override Pattern ZeroOrMore
        {
            get
            {
                return base.ZeroOrMore.RegEx("?");
            }
        }

        public override Pattern OneOrMore
        {
            get
            {
                return base.OneOrMore.RegEx("?");
            }
        }

        public override Pattern Exactly(int timesToRepeat)
        {
            return base.Exactly(timesToRepeat).RegEx("?");
        }

        public override Pattern AtLeast(int timesToRepeat)
        {
            return base.AtLeast(timesToRepeat).RegEx("?");
        }

        public override Pattern Optional
        {
            get
            {
                return base.Optional.RegEx("?");
            }
        }

        public override Pattern InRange(int minimum, int maximum)
        {
            return base.InRange(minimum, maximum).RegEx("?");
        }

        public override Pattern AtMost(int timesToRepeat)
        {
            throw new InvalidOperationException("You cannot perform lazy evaluation of the AtMost {,n} quantifier.");
        }
    }
}
