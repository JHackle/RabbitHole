namespace Hackle.Map
{
    using Hackle.Util;
    using NUnit.Framework;
    using UnityEngine;

    public class MapGeneratorTest
    {

        private GameObject waterPrefab;
        private GameObject grassPrefab;
        private GameObject desertPrefab;
        private GameObject mountainPrefab;
        private GameObject forestPrefab;

        private MapGenerator mapGenerator;

        [SetUp]
        public void SetUp()
        {
            waterPrefab = UnityEngine.Resources.Load("Tests/WaterTile") as GameObject;
            waterPrefab.tag = "WaterTile";
            grassPrefab = UnityEngine.Resources.Load("Tests/GrassTile") as GameObject;
            grassPrefab.tag = "GrassTile";
            desertPrefab = UnityEngine.Resources.Load("Tests/DesertTile") as GameObject;
            desertPrefab.tag = "DesertTile";
            mountainPrefab = UnityEngine.Resources.Load("Tests/MountenTile") as GameObject;
            mountainPrefab.tag = "MountainTile";
            forestPrefab = UnityEngine.Resources.Load("Tests/ForestTile") as GameObject;
            forestPrefab.tag = "ForestTile";

            mapGenerator = new GameObject().AddComponent<MapGenerator>();
            mapGenerator.WaterPrefab = waterPrefab.transform;
            mapGenerator.GrassPrefab = grassPrefab.transform;
            mapGenerator.DesertPrefab = desertPrefab.transform;
            mapGenerator.MountainPrefab = mountainPrefab.transform;
            mapGenerator.ForestPrefab = forestPrefab.transform;
        }


        [TearDown]
        public void CleanUp()
        {
            GameObject.Destroy(mapGenerator.gameObject);
        }

        [Test]
        public void Water100Percent()
        {
            Constants.MapSettings = CreateSettings(10, 10, 1, 0, 0, 0, 0);

            mapGenerator.GenerateMap();

            Validate(100, 0, 0, 0, 0);
        }


        [Test]
        public void Grass100Percent()
        {
            Constants.MapSettings = CreateSettings(10, 10, 0, 1, 0, 0, 0);

            mapGenerator.GenerateMap();

            Validate(0, 100, 0, 0, 0);
        }

        [Test]
        public void Desert100Percent()
        {
            Constants.MapSettings = CreateSettings(10, 10, 0, 0, 1, 0, 0);

            mapGenerator.GenerateMap();

            Validate(0, 0, 100, 0, 0);
        }

        [Test]
        public void Mountain100Percent()
        {
            Constants.MapSettings = CreateSettings(10, 10, 0, 0, 0, 1, 0);

            mapGenerator.GenerateMap();

            Validate(0, 0, 0, 100, 0);
        }

        [Test]
        public void Forest100Percent()
        {
            Constants.MapSettings = CreateSettings(10, 10, 0, 0, 0, 0, 1);

            mapGenerator.GenerateMap();

            Validate(0, 0, 0, 0, 100);
        }

        private void Validate(int water, int grass, int desert, int mountain, int forest)
        {
            GameObject[] waterTiles = GameObject.FindGameObjectsWithTag("WaterTile");
            GameObject[] grassTiles = GameObject.FindGameObjectsWithTag("GrassTile");
            GameObject[] desertTiles = GameObject.FindGameObjectsWithTag("DesertTile");
            GameObject[] mountainTiles = GameObject.FindGameObjectsWithTag("MountainTile");
            GameObject[] forestTiles = GameObject.FindGameObjectsWithTag("ForestTile");

            Assert.AreEqual(water, waterTiles.Length);
            Assert.AreEqual(grass, grassTiles.Length);
            Assert.AreEqual(desert, desertTiles.Length);
            Assert.AreEqual(mountain, mountainTiles.Length);
            Assert.AreEqual(forest, forestTiles.Length);
        }

        private static MapSettings CreateSettings(int x, int y, float water, float grass, float desert, float mountain, float forest)
        {
            MapSettings settings = new MapSettings();
            settings.MapSize = new Coord(x, y);
            settings.WaterPercent = water;
            settings.GrassPercent = grass;
            settings.DesertPercent = desert;
            settings.MountainPercent = mountain;
            settings.ForestPercent = forest;
            return settings;
        }
    }
}