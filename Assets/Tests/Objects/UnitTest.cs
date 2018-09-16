namespace Hackle.Objects
{
    using Hackle.Map;
    using NUnit.Framework;

    public class UnitTest
    {
        [Test]
        public void PositionTest()
        {
            Unit u = new Unit();
            u.xPos = 3;
            u.yPos = 4;
            Coord c = u.Position();

            Assert.AreEqual(new Coord(3, 4), c);
        }
    }
}
