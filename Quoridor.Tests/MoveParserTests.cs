namespace Quoridor.Tests
{
    using Core.Field;
    using Core.Moves;
    using NUnit.Framework;

    [TestFixture]
    public class MoveParserTests
    {
        private readonly IMoveParser moveParser = new MoveParser(new PositionParser());
        
        [Test]
        public void _01ShouldParsePawnStepMove()
        {
            var move = moveParser.ParseMove("move E1", Color.Black);
            Assert.That(move, Is.TypeOf<PawnStepMove>());
        }
        
        [Test]
        public void _02ShouldParsePlaceWallMove()
        {
            var move = moveParser.ParseMove("wall X2h", Color.Black);
            Assert.That(move, Is.TypeOf<PlaceWallMove>());
        }
        
        [Test]
        public void _03ShouldParseJumpMove()
        {
            var move = moveParser.ParseMove("jump E1", Color.Black);
            Assert.That(move, Is.TypeOf<JumpMove>());
        }
        
        [TestCase("jumpe C1")]
        [TestCase("jump C1 C2")]
        [TestCase("wall E3")]
        [TestCase("jump A1h")]
        public void _03ShouldParseUnknownMove(string code)
        {
            var move = moveParser.ParseMove(code, Color.Black);
            Assert.That(move, Is.TypeOf<UnknownMove>());
        }
    }
}