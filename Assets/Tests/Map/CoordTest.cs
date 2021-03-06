﻿namespace Hackle.Map
{
    using NUnit.Framework;

    public class CoordTest
    {
        [Test]
        public void ConstructorTest()
        {
            Coord c = new Coord();
            Assert.AreEqual(0, c.X);
            Assert.AreEqual(0, c.Y);

            Coord c2 = new Coord(3, 5);
            Assert.AreEqual(3, c2.X);
            Assert.AreEqual(5, c2.Y);
        }

        [Test]
        public void EqualTest()
        {
            Coord coord1 = new Coord(1, 2);
            Coord coord2 = new Coord(2, 2);
            Coord coord3 = new Coord(2, 2);

            Assert.IsFalse(coord1.Equals(coord2));
            Assert.IsTrue(coord2.Equals(coord3));

            Assert.IsFalse(coord1 == coord2);
            Assert.IsTrue(coord2 == coord3);
        }
    }
}
