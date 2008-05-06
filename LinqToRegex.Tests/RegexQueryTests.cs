 using FlimFlan.ReadableRex;
using NUnit.Framework;
using Osherove.LinqToRegex;

namespace Osherove.LinqToRegex.Tests
{
    [TestFixture]
    public class RegexQueryTests
    {
        [Test]
        public void FindEmailUsingPattern()
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
                Assert.AreEqual("Email@somewhere.com",match.Value);
            }
        }
        
        
        [Test]
        public void CanUseSimpleConstructs()
        {
            var query = from match in RegexQuery.Against("sdlfjsfl43r3490r98*(*Email@somewhere.com_dakj3j")
                        where match.AtBeginning.Literal("s")
                            .NonDigit.Repeat.AtLeast(3)
                            .Digit
                            .IsTrue()
                        select match;
            foreach (var match in query)
            {
                Assert.AreEqual("sdlfjsfl4", match.Value);
            }
        }
        
        [Test]
        public void CanUseGrouping()
        {
            var query = from match in RegexQuery.Against("sdlfjsfl43r3490r98*(*Email@somewhere.com_dakj3j")
                        where Pattern.With
                            .NamedGroup("groupa",
                                        Pattern.With.AtBeginning.Literal("s")
                                            .NonDigit.Repeat.AtLeast(3)
                                            .Digit)
                            .IsTrue()
                        select match;
            foreach (var match in query)
            {
                Assert.AreEqual("sdlfjsfl4", match.Value);
                Assert.AreEqual("sdlfjsfl4", match.Groups["groupa"].Value);
            }
        }
    }
}