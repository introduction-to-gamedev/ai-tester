namespace Quoridor.Tests
{
    using Core.Field;
    using Core.Moves;
    using IntroToGameDev.AiTester.Utils;
    using NUnit.Framework;
    
    [TestFixture]
    public class MoveTests
    {
        [Test]
        public void _01PawnStepMoveIsValidated()
        {
            var field = new QuoridorField();
            
            field.MovePawnTo(Color.Black, (7, 4));
            
            Assert.That(new PawnStepMove(Color.White, new Position(8, 5)).Validate(field).IsValid, Is.True);
            Assert.That(new PawnStepMove(Color.White, new Position(8, 3)).Validate(field).IsValid, Is.True);
            
            Assert.That(new PawnStepMove(Color.White, new Position(7, 4)).Validate(field).IsValid, Is.False);
            Assert.That(new PawnStepMove(Color.White, new Position(8, 4)).Validate(field).IsValid, Is.False);
            Assert.That(new PawnStepMove(Color.White, new Position(1, 1)).Validate(field).IsValid, Is.False);
        }
        
        [Test]
        public void _01ForbidPlacingTwoWallsInSamePosition()
        {
            var field = new QuoridorField();

            var placeWallMove = new PlaceWallMove(Color.Black, new Position(1,1), WallType.Horizontal);
            Assert.That(placeWallMove.Validate(field).IsValid, Is.True);
            placeWallMove.Execute(field);
            Assert.That(new PlaceWallMove(Color.Black, new Position(1,1), WallType.Vertical).Validate(field).IsValid, Is.False);
        }
        
        [Test]
        public void _01ForbidPlacingTwoManyWalls()
        {
            var field = new QuoridorField();

            for (int i = 0; i < 5; i++)
            {
                var placeWallMove = new PlaceWallMove(Color.Black, new Position(i,0), WallType.Horizontal);
                Assert.That(placeWallMove.Validate(field).IsValid, Is.True); 
                placeWallMove.Execute(field);

                placeWallMove = new PlaceWallMove(Color.Black, new Position(i, 2), WallType.Horizontal);
                Assert.That(placeWallMove.Validate(field).IsValid, Is.True);
                placeWallMove.Execute(field);
            }
            
            Assert.That(new PlaceWallMove(Color.Black, new Position(7,7), WallType.Vertical).Validate(field).IsValid, Is.False);
        }
    }
}