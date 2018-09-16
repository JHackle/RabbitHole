namespace Hackle.Map
{
    using Hackle.Util;
    using NUnit.Framework;
    using System;
    using UnityEngine;

    public class TileGeneratorTest
    {
        Transform waterPrefab;
        Transform grassPrefab;
        Transform desertPrefab;
        Transform mountainPrefab;
        Transform forestPrefab;

        [SetUp]
        public void SetUp()
        {
            waterPrefab = new GameObject().transform;
            waterPrefab.tag = "WaterTile";
            grassPrefab = new GameObject().transform;
            grassPrefab.tag = "GrassTile";
            desertPrefab = new GameObject().transform;
            desertPrefab.tag = "DesertTile";
            mountainPrefab = new GameObject().transform;
            mountainPrefab.tag = "MountainTile";
            forestPrefab = new GameObject().transform;
            forestPrefab.tag = "ForestTile";
        }

        [Test]
        public void GenerateOneTerrain()
        {
            MapSettings[] settings = new MapSettings[]
            {
            CreateSettings(10, 10, 1, 0, 0, 0, 0),
            CreateSettings(10, 10, 0, 1, 0, 0, 0),
            CreateSettings(10, 10, 0, 0, 1, 0, 0),
            CreateSettings(10, 10, 0, 0, 0, 1, 0),
            CreateSettings(10, 10, 0, 0, 0, 0, 1)
            };
            int[][] tilesExpected = new int[][]
            {
            new int[]{100, 0, 0, 0, 0 },
            new int[]{0, 100, 0, 0, 0 },
            new int[]{0, 0, 100, 0, 0 },
            new int[]{0, 0, 0, 100, 0 },
            new int[]{0, 0, 0, 0, 100 },
            };

            ExecuteTest(settings, tilesExpected);
        }

        [Test]
        public void GenerateTwoTerrains()
        {
            MapSettings[] settings = new MapSettings[]
            {
            CreateSettings(10, 10, 1, 1, 0, 0, 0),
            CreateSettings(10, 10, 0, 1, 1, 0, 0),
            CreateSettings(10, 10, 0, 0, 1, 1, 0),
            CreateSettings(10, 10, 0, 0, 0, 1, 1),
            CreateSettings(10, 10, 1, 0, 0, 0, 1)
            };
            int[][] tilesExpected = new int[][]
            {
            new int[]{50, 50, 0, 0, 0 },
            new int[]{0, 50, 50, 0, 0 },
            new int[]{0, 0, 50, 50, 0 },
            new int[]{0, 0, 0, 50, 50 },
            new int[]{50, 0, 0, 0, 50 },
            };

            ExecuteTest(settings, tilesExpected);
        }

        [Test]
        public void GenerateThreeTerrains()
        {
            MapSettings[] settings = new MapSettings[]
            {
            CreateSettings(10, 10, 1, 1, 1, 0, 0),
            CreateSettings(10, 10, 0, 1, 1, 1, 0),
            CreateSettings(10, 10, 0, 0, 1, 1, 1),
            CreateSettings(10, 10, 1, 0, 0, 1, 1),
            CreateSettings(10, 10, 1, 1, 0, 0, 1)
            };
            int[][] tilesExpected = new int[][]
            {
            new int[]{34, 33, 33, 0, 0 },
            new int[]{1, 33, 33, 33, 0 },
            new int[]{1, 0, 33, 33, 33 },
            new int[]{34, 0, 0, 33, 33 },
            new int[]{34, 33, 0, 0, 33 },
            };

            ExecuteTest(settings, tilesExpected);
        }

        [Test]
        public void GenerateFourTerrains()
        {
            MapSettings[] settings = new MapSettings[]
            {
            CreateSettings(10, 10, 1, 1, 1, 1, 0),
            CreateSettings(10, 10, 0, 1, 1, 1, 1),
            CreateSettings(10, 10, 1, 0, 1, 1, 1),
            CreateSettings(10, 10, 1, 1, 0, 1, 1),
            CreateSettings(10, 10, 1, 1, 1, 0, 1)
            };
            int[][] tilesExpected = new int[][]
            {
            new int[]{25, 25, 25, 25, 0 },
            new int[]{0, 25, 25, 25, 25 },
            new int[]{25, 0, 25, 25, 25 },
            new int[]{25, 25, 0, 25, 25 },
            new int[]{25, 25, 25, 0, 25 },
            };

            ExecuteTest(settings, tilesExpected);
        }

        [Test]
        public void GenerateFiveTerrains()
        {
            MapSettings[] settings = new MapSettings[]
            {
            CreateSettings(10, 10, 1, 1, 1, 1, 1),
            CreateSettings(10, 10, 1, 1, 1, 1, 1),
            CreateSettings(10, 10, 1, 1, 1, 1, 1),
            CreateSettings(10, 10, 1, 1, 1, 1, 1),
            CreateSettings(10, 10, 1, 1, 1, 1, 1)
            };
            int[][] tilesExpected = new int[][]
            {
            new int[]{20, 20, 20, 20, 20 },
            new int[]{20, 20, 20, 20, 20 },
            new int[]{20, 20, 20, 20, 20 },
            new int[]{20, 20, 20, 20, 20 },
            new int[]{20, 20, 20, 20, 20 },
            };

            ExecuteTest(settings, tilesExpected);
        }

        [Test]
        public void GenerateLargeMaps()
        {
            MapSettings[] settings = new MapSettings[]
            {
            CreateSettings(100, 100, 1, 1, 1, 1, 1),
            CreateSettings(100, 1, 1, 1, 1, 1, 1),
            CreateSettings(1, 100, 1, 1, 1, 1, 1),
            };
            int[][] tilesExpected = new int[][]
            {
            new int[]{2000, 2000, 2000, 2000, 2000 },
            new int[]{20, 20, 20, 20, 20 },
            new int[]{20, 20, 20, 20, 20 },
            };

            ExecuteTest(settings, tilesExpected);
        }

        [Test]
        public void AllTileTypesNull()
        {
            MapSettings[] settings = new MapSettings[]
            {
            CreateSettings(50, 50, 0, 0, 0, 0, 0),
            };
            int[][] tilesExpected = new int[][]
            {
            new int[]{20, 20, 20, 20, 20 },
            };
            Assert.Throws<InvalidOperationException>(() => ExecuteTest(settings, tilesExpected));
        }

        [Test]
        public void GenerateRandomMap()
        {
            Constants.MapSettings = CreateSettings(10, 10, 1, 1, 0, 0, 0);
            TileGenerator generator = new TileGenerator(waterPrefab, grassPrefab, desertPrefab, mountainPrefab, forestPrefab);
            Transform[,] tiles = generator.GenerateTiles();

            // if shuffeling doesn't work, the first 50 tiles would be water
            bool isGrass = false;
            int halfMap = 5;
            for (int x = 0; x < halfMap; x++)
            {
                for (int y = 0; y < halfMap; y++)
                {
                    isGrass |= tiles[x, y].tag.Equals("GrassTile");
                }
            }

            Assert.IsTrue(isGrass);
        }

        private void ExecuteTest(MapSettings[] settings, int[][] tilesExpected)
        {
            for (int index = 0; index < settings.Length; index++)
            {
                Constants.MapSettings = settings[index];
                TileGenerator generator = new TileGenerator(waterPrefab, grassPrefab, desertPrefab, mountainPrefab, forestPrefab);
                Transform[,] tiles = generator.GenerateTiles();
                Validate(tiles, tilesExpected[index]);
            }
        }

        private void Validate(Transform[,] tiles, int[] expected)
        {
            int water = 0;
            int grass = 0;
            int desert = 0;
            int mountain = 0;
            int forest = 0;

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    switch (tiles[x, y].tag)
                    {
                        case "WaterTile":
                            water++;
                            break;
                        case "GrassTile":
                            grass++;
                            break;
                        case "DesertTile":
                            desert++;
                            break;
                        case "MountainTile":
                            mountain++;
                            break;
                        case "ForestTile":
                            forest++;
                            break;
                    }
                }
            }

            Assert.AreEqual(expected[0], water);
            Assert.AreEqual(expected[1], grass);
            Assert.AreEqual(expected[2], desert);
            Assert.AreEqual(expected[3], mountain);
            Assert.AreEqual(expected[4], forest);
        }

        private static MapSettings CreateSettings(int x, int y, float water, float grass, float desert, float mountain, float forest)
        {
            MapSettings settings = new MapSettings();
            settings.mapSize.x = x;
            settings.mapSize.y = y;
            settings.waterPercent = water;
            settings.grassPercent = grass;
            settings.desertPercent = desert;
            settings.mountainPercent = mountain;
            settings.forestPercent = forest;
            return settings;
        }
    }
}