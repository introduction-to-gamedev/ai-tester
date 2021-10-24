namespace Quoridor.Tests
{
    using Core.Field;
    using IntroToGameDev.AiTester.Utils;
    using NUnit.Framework;

    [TestFixture]
    public class PositionParserTests
    {
        private readonly IPositionConverter positionConverter = new PositionConverter();

        [Test]
        public void _01ShouldParseCells()
        {
            Assert.That(positionConverter.TryParseCellPosition("A1"), Is.EqualTo(new Position(0,0)));
            Assert.That(positionConverter.TryParseCellPosition("I9"), Is.EqualTo(new Position(8,8)));
            
            Assert.That(positionConverter.TryParseCellPosition("J9"), Is.Null);
            Assert.That(positionConverter.TryParseCellPosition("A0"), Is.Null);
            Assert.That(positionConverter.TryParseCellPosition("B10"), Is.Null);
        }
        
        [Test]
        public void _02ShouldParseWalls()
        {
            Assert.That(positionConverter.TryParseWallPosition("S1h"), Is.EqualTo((new Position(0,0), WallType.Horizontal)));
            Assert.That(positionConverter.TryParseWallPosition("Z8v"), Is.EqualTo((new Position(7,7), WallType.Vertical)));
            
            Assert.That(positionConverter.TryParseWallPosition("S1"), Is.Null);
            Assert.That(positionConverter.TryParseWallPosition("R4h"), Is.Null);
            Assert.That(positionConverter.TryParseWallPosition("Z9v"), Is.Null);
            Assert.That(positionConverter.TryParseWallPosition("G3v"), Is.Null);
        }
    }
}