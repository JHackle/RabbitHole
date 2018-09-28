namespace Hackle.Objects
{
    using Hackle.Map;
    using Hackle.Objects;
    using NUnit.Framework;

    public class ObjectTest
    {
        [Test]
        public void PositionTest()
        {
            Object u = new Object();
            u.Type = ObjectType.Tile;
            u.Position = new Coord(3, 4);
            Coord c = u.Position;

            Assert.AreEqual(new Coord(3, 4), c);
        }
    }
}
