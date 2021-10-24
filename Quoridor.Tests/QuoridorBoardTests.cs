namespace Quoridor.Tests
{
    using Core.Field;
    using IntroToGameDev.AiTester.Utils;
    using NUnit.Framework;

    public class QuoridorBoardTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AfterCreation_PawnsShouldBeOnRightPositions()
        {
            var field = new QuoridorField();
            Assert.That(field.GetCellWithPawn(Color.Black).Position, Is.EqualTo(new Position(0, 4)));
            Assert.That(field.GetCellWithPawn(Color.White).Position, Is.EqualTo(new Position(8, 4)));
        }
        
        [Test]
        public void ShouldReturnCellOrNull()
        {
            var field = new QuoridorField();
            Assert.That(field.GetCell(new Position(0,0)).Position, Is.EqualTo(new Position(0,0)));
            Assert.That(field.GetCell(new Position(3,4)).Position, Is.EqualTo(new Position(3,4)));
            
            Assert.That(field.GetCell(new Position(-3,4)), Is.Null);
            Assert.That(field.GetCell(new Position(9,9)), Is.Null);
        }
    }
}