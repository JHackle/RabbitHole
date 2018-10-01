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
            u.Type = ObjectType.GrassTile;
            Assert.AreEqual(ObjectType.GrassTile, u.Type);
        }
    }
}
