namespace Hackle.Util
{
    using NUnit.Framework;
    using UnityEngine;
    using Hackle.Map;

    class TileUtilTest
    {
        private MapSettings mapSettings;
        private int tileSize;
        private Coord mapSize;

        [SetUp]
        public void SetUp()
        {
            mapSettings = Constants.MapSettings;
            tileSize = mapSettings.TileSize;
            mapSize = mapSettings.MapSize;
        }

        [Test]
        public void CoordToPositionTest()
        {
            for (int x = 0; x < mapSize.X; x++)
            {
                for (int y = 0; y < mapSize.Y; y++)
                {
                    ValidateCoordToPosition(x, y);
                }
            }
        }

        [Test]
        public void CoordToPositionIllegal1()
        {
            ValidateInt(-1, 0);
            ValidateCoord(-1, 0);
        }

        [Test]
        public void CoordToPositionIllegal2()
        {
            ValidateInt(0, -1);
            ValidateCoord(0, -1);
        }

        [Test]
        public void CoordToPositionIllegal3()
        {
            ValidateInt(mapSize.X + 1, 0);
            ValidateCoord(mapSize.X + 1, 0);
        }

        [Test]
        public void CoordToPositionIllegal4()
        {
            ValidateInt(0, mapSize.Y + 1);
            ValidateCoord(0, mapSize.Y + 1);
        }

        [Test]
        public void RandomPositionTest()
        {
            Coord pos = TileUtil.RandomPosition();
            int length = 1000;
            for (int i = 0; i < length; i++)
            {
                Coord c = TileUtil.RandomPosition();
                if (!c.Equals(pos))
                {
                    // random generation works at least a bit
                    return;
                }
            }
            Assert.Fail();
        }

        private static void ValidateInt(int x, int y)
        {
            try
            {
                TileUtil.CoordToPosition(x, y);
            }
            catch (CoordinateTranslationException)
            {
                // this exception is being expected
                return;
            }
            Assert.Fail();
        }

        private static void ValidateCoord(int x, int y)
        {
            try
            {
                TileUtil.CoordToPosition(new Coord(x, y));
            }
            catch (CoordinateTranslationException)
            {
                // this exception is being expected
                return;
            }
            Assert.Fail();
        }

        private void ValidateCoordToPosition(int x, int y)
        {
            float expectedX = -mapSize.X / 2f + 0.5f * tileSize + x * tileSize;
            float expectedY = -mapSize.Y / 2f + 0.5f * tileSize + y * tileSize;

            Vector3 v1 = TileUtil.CoordToPosition(new Coord(x, y));
            Vector3 v2 = TileUtil.CoordToPosition(x, y);

            Assert.IsTrue(v1.Equals(v2));
            Assert.AreEqual(new Vector3(expectedX, 0, expectedY), v1);
        }
    }
}
