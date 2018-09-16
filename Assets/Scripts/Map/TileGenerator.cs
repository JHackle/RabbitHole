namespace Hackle.Map
{
    using Hackle.Util;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class TileGenerator
    {
        private readonly MapSettings mapSettings;
        private readonly Transform waterPrefab;
        private readonly Transform grassPrefab;
        private readonly Transform desertPrefab;
        private readonly Transform mountainPrefab;
        private readonly Transform forestPrefab;

        public TileGenerator(Transform waterPrefab, Transform grassPrefab, Transform desertPrefab, Transform mountainPrefab, Transform forestPrefab)
        {
            this.mapSettings = Constants.MapSettings;
            this.waterPrefab = waterPrefab;
            this.grassPrefab = grassPrefab;
            this.desertPrefab = desertPrefab;
            this.mountainPrefab = mountainPrefab;
            this.forestPrefab = forestPrefab;
        }

        public Transform[,] GenerateTiles()
        {
            // calculate how many tiles of each kind are required
            int numOfTiles = mapSettings.mapSize.x * mapSettings.mapSize.y;
            float sum = mapSettings.CalculateTilesSum();
            if (sum <= 0)
            {
                throw new InvalidOperationException("There are no tile types specified. Unable to create map");
            }
            int water = (int)(mapSettings.waterPercent / sum * numOfTiles);
            int grass = (int)(mapSettings.grassPercent / sum * numOfTiles);
            int desert = (int)(mapSettings.desertPercent / sum * numOfTiles);
            int mountain = (int)(mapSettings.mountainPercent / sum * numOfTiles);
            int forest = (int)(mapSettings.forestPercent / sum * numOfTiles);

            // fix possible rounding error
            if (numOfTiles != (water + grass + desert + mountain + forest))
            {
                water = numOfTiles - (grass + desert + mountain + forest);
                // TODO : can water be negativ?
            }

            // add all tiles to a list
            List<Transform> transforms = CreateListOfAllTiles(water, grass, desert, mountain, forest);

            // shuffle the list of tiles
            ShuffleList(transforms);

            // creat an array of random tiles
            Transform[,] tiles = ConverToArray(transforms);

            return tiles;
        }

        private Transform[,] ConverToArray(List<Transform> transforms)
        {
            Transform[,] tiles = new Transform[mapSettings.mapSize.x, mapSettings.mapSize.y];
            int listIndex = 0;
            for (int x = 0; x < mapSettings.mapSize.x; x++)
            {
                for (int y = 0; y < mapSettings.mapSize.y; y++)
                {
                    if (listIndex > transforms.Count)
                    {
                        throw new InvalidOperationException("There are too much tiles! " + listIndex + " " + transforms.Count);
                    }
                    tiles[x, y] = transforms.ElementAt<Transform>(listIndex);
                    listIndex++;
                }
            }

            return tiles;
        }

        private List<Transform> CreateListOfAllTiles(int water, int grass, int desert, int mountain, int forest)
        {
            List<Transform> transforms = new List<Transform>();
            for (int i = 0; i < water; i++)
            {
                transforms.Add(waterPrefab);
            }
            for (int i = 0; i < grass; i++)
            {
                transforms.Add(grassPrefab);
            }
            for (int i = 0; i < desert; i++)
            {
                transforms.Add(desertPrefab);
            }
            for (int i = 0; i < mountain; i++)
            {
                transforms.Add(mountainPrefab);
            }
            for (int i = 0; i < forest; i++)
            {
                transforms.Add(forestPrefab);
            }

            return transforms;
        }

        private static void ShuffleList(List<Transform> transforms)
        {
            System.Random rnd = new System.Random();
            int n = transforms.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Transform value = transforms[k];
                transforms[k] = transforms[n];
                transforms[n] = value;
            }
        }
    }
}