namespace Quoridor.Tests
{
    using System;
    using Core.Field;
    using Core.Pathfinder;
    using NUnit.Framework;
    
    [TestFixture]
    public class PathFinderTests
    {
        [Test]
        public void ShouldFindWay()
        {
            var field = new QuoridorField();
            var pathfinder = new AStarPathFinder<ICell>((a, b) => 1);

            var path = pathfinder.FindPath(field.GetCell((0,0)), field.GetCell((8,8)));
            Assert.That(path, Is.Not.Null);
            Assert.That(path.Count, Is.EqualTo(16));
        }
        
        [Test]
        public void WallsShouldBlockWay()
        {
            var field = new QuoridorField();
            field.PlaceWall(new Wall(WallType.Horizontal, (0,0), Color.Black));
            field.PlaceWall(new Wall(WallType.Horizontal, (0,2), Color.Black));
            field.PlaceWall(new Wall(WallType.Horizontal, (0,4), Color.Black));
            field.PlaceWall(new Wall(WallType.Horizontal, (0,6), Color.Black));
            
            field.PlaceWall(new Wall(WallType.Horizontal, (1,7), Color.Black));
            field.PlaceWall(new Wall(WallType.Vertical, (1,6), Color.Black));
            
            var pathfinder = new AStarPathFinder<ICell>((a, b) => 1);

            var path = pathfinder.FindPath(field.GetCell((0,0)), field.GetCell((8,0)));
            Assert.That(path, Is.Null);
        }
    }
}