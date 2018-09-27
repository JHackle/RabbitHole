namespace Hackle.Objects
{
    using Hackle.Map;
    using Hackle.Objects;
    using NUnit.Framework;

    public class UnitTest
    {
        [Test]
        public void PositionTest()
        {
            Unit u = new Unit();
            u.Type = UnitType.Tile;
            u.X = 3;
            u.Y = 4;
            Coord c = u.Position();

            Assert.AreEqual(new Coord(3, 4), c);
        }
    }
}
