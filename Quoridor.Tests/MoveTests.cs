namespace Quoridor.Tests
{
    using Core.Field;
    using Core.Moves;
    using IntroToGameDev.AiTester.Utils;
    using NUnit.Framework;
    
    [TestFixture]
    public class MoveTests
    {
        private IQuoridorField field;

        private IPositionParser positionParser = new PositionParser();

        [SetUp]
        public void SetUp()
        {
            field = new QuoridorField();
        }
        
        private void ValidateAndExecute(Move move)
        {
            Assert.That(move.Validate(field).IsValid, Is.True);
            move.Execute(field);
        }
        
        private void Validate(Move move, bool result)
        {
            Assert.That(move.Validate(field).IsValid, Is.EqualTo(result));
        }
        
        [Test]
        public void PawnStepMoveIsValidated()
        {
            field.MovePawnTo(Color.Black, (7, 4));
            
            Assert.That(new PawnStepMove(Color.White, new Position(8, 5)).Validate(field).IsValid, Is.True);
            Assert.That(new PawnStepMove(Color.White, new Position(8, 3)).Validate(field).IsValid, Is.True);
            
            Assert.That(new PawnStepMove(Color.White, new Position(7, 4)).Validate(field).IsValid, Is.False);
            Assert.That(new PawnStepMove(Color.White, new Position(8, 4)).Validate(field).IsValid, Is.False);
            Assert.That(new PawnStepMove(Color.White, new Position(1, 1)).Validate(field).IsValid, Is.False);
        }
        
        [Test]
        public void ForbidPlacingTwoWallsInSamePosition()
        {
            var placeWallMove = new PlaceWallMove(Color.Black, new Position(1,1), WallType.Horizontal);
            Assert.That(placeWallMove.Validate(field).IsValid, Is.True);
            placeWallMove.Execute(field);
            Assert.That(new PlaceWallMove(Color.Black, new Position(1,1), WallType.Vertical).Validate(field).IsValid, Is.False);
        }
        
        [Test]
        public void ForbidPlacingTwoManyWalls()
        {
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
        
        [Test]
        public void ForbidPlacingTwoWallsOfOneDirectionCloseToOne()
        {
            var move = new PlaceWallMove(Color.Black, new Position(0,0), WallType.Horizontal);
            move.Execute(field);
            
            Assert.That(new PlaceWallMove(Color.Black, new Position(0,1), WallType.Horizontal).Validate(field).IsValid, Is.False);
            Assert.That(new PlaceWallMove(Color.Black, new Position(0,1), WallType.Vertical).Validate(field).IsValid, Is.True);
        }
        
        [Test]
        public void MoveOverWallIsForbidden()
        {
            ValidateAndExecute(new PlaceWallMove(Color.Black, new Position(7,4), WallType.Horizontal));
            ValidateAndExecute(new PlaceWallMove(Color.Black, new Position(7,3), WallType.Vertical));
            
            Assert.That(new PawnStepMove(Color.White, new Position(7, 4)).Validate(field).IsValid, Is.False);
            Assert.That(new PawnStepMove(Color.White, new Position(8, 3)).Validate(field).IsValid, Is.False);
            Assert.That(new PawnStepMove(Color.White, new Position(8, 5)).Validate(field).IsValid, Is.True);
        }

        [Test]
        public void AllowCorrectJumpMoves()
        {
            field.MovePawnTo(Color.Black, GetCellPosition("E8"));
            Validate(new JumpMove(Color.White, GetCellPosition("E7")), true);
            Validate(new JumpMove(Color.White, GetCellPosition("D7")), false);
            Validate(new JumpMove(Color.White, GetCellPosition("F7")), false);
        }
        
        [Test]
        public void AllowCorrectHorizntallyBlockedJumpMoves()
        {
            field.MovePawnTo(Color.Black, GetCellPosition("E8"));
            field.PlaceWall(new Wall(WallType.Horizontal, GetWallPosition("w7h"), Color.Black));
            
            Validate(new JumpMove(Color.White, GetCellPosition("E7")), false);
            Validate(new JumpMove(Color.White, GetCellPosition("D8")), true);
            Validate(new JumpMove(Color.White, GetCellPosition("F8")), true);
        }
        
        [Test]
        public void AllowCorrectVerticallyBlockedJumpMoves()
        {
            field.MovePawnTo(Color.Black, GetCellPosition("F9"));
            field.PlaceWall(new Wall(WallType.Vertical, GetWallPosition("x8v"), Color.Black));
            
            Validate(new JumpMove(Color.White, GetCellPosition("G9")), false);
            Validate(new JumpMove(Color.White, GetCellPosition("F8")), true);
        }
        
        [Test]
        public void WayBlockingShouldBeForbidden()
        {
            field.PlaceWall(new Wall(WallType.Horizontal, (0,0), Color.Black));
            field.PlaceWall(new Wall(WallType.Horizontal, (0,2), Color.Black));
            field.PlaceWall(new Wall(WallType.Horizontal, (0,4), Color.Black));
            field.PlaceWall(new Wall(WallType.Horizontal, (0,6), Color.Black));
            
            field.PlaceWall(new Wall(WallType.Horizontal, (1,7), Color.Black));
            
            Validate(new PlaceWallMove(Color.White, (1, 6), WallType.Vertical), false);
        }

        private Position GetCellPosition(string code)
        {
            return positionParser.TryParseCellPosition(code).Value;
        }
        
        private Position GetWallPosition(string code)
        {
            return positionParser.TryParseWallPosition(code).Value.position;
        }
    }
}