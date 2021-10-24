namespace Quoridor.Tests
{
    using Core.Field;
    using Core.Moves;
    using NUnit.Framework;

    [TestFixture]
    public class MoveParserTests
    {
        private readonly IMoveConverter moveConverter = new MoveConverter(new PositionConverter());
        
        [Test]
        public void _01ShouldParsePawnStepMove()
        {
            var code = "move E1";
            var move = moveConverter.ParseMove(code, Color.Black);
            Assert.That(move, Is.TypeOf<PawnStepMove>());
            Assert.That(moveConverter.GetCode(move), Is.EqualTo(code));
        }
        
        [Test]
        public void _02ShouldParsePlaceWallMove()
        {
            var code = "wall X2h";
            var move = moveConverter.ParseMove(code, Color.Black);
            Assert.That(move, Is.TypeOf<PlaceWallMove>());
            Assert.That(moveConverter.GetCode(move), Is.EqualTo(code));
        }
        
        [Test]
        public void _03ShouldParseJumpMove()
        {
            var code = "jump E1";
            var move = moveConverter.ParseMove(code, Color.Black);
            Assert.That(move, Is.TypeOf<JumpMove>());
            Assert.That(moveConverter.GetCode(move), Is.EqualTo(code));
        }
        
        [TestCase("jumpe C1")]
        [TestCase("jump C1 C2")]
        [TestCase("wall E3")]
        [TestCase("jump A1h")]
        public void _03ShouldParseUnknownMove(string code)
        {
            var move = moveConverter.ParseMove(code, Color.Black);
            Assert.That(move, Is.TypeOf<UnknownMove>());
        }
    }
}