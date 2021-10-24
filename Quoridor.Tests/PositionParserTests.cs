namespace Quoridor.Tests
{
    using Core.Field;
    using IntroToGameDev.AiTester.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class PositionParserTests
    {
        private readonly IPositionParser positionParser = new PositionParser();

        [Test]
        public void _01ShouldParseCells()
        {
            Assert.That(positionParser.TryParseCellPosition("A1"), Is.EqualTo(new Position(0,0)));
            Assert.That(positionParser.TryParseCellPosition("I9"), Is.EqualTo(new Position(8,8)));
            
            Assert.That(positionParser.TryParseCellPosition("J9"), Is.Null);
            Assert.That(positionParser.TryParseCellPosition("A0"), Is.Null);
            Assert.That(positionParser.TryParseCellPosition("B10"), Is.Null);
        }
        
        [Test]
        public void _02ShouldParseWalls()
        {
            Assert.That(positionParser.TryParseWallPosition("S1h"), Is.EqualTo((new Position(0,0), WallType.Horizontal)));
            Assert.That(positionParser.TryParseWallPosition("Z8v"), Is.EqualTo((new Position(7,7), WallType.Vertical)));
            
            Assert.That(positionParser.TryParseWallPosition("S1"), Is.Null);
            Assert.That(positionParser.TryParseWallPosition("R4h"), Is.Null);
            Assert.That(positionParser.TryParseWallPosition("Z9v"), Is.Null);
            Assert.That(positionParser.TryParseWallPosition("G3v"), Is.Null);
        }
    }
}