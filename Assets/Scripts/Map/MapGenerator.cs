namespace Hackle.Map
{
    using System;
    using Hackle.Objects;
    using Hackle.Util;
    using UnityEngine;

    public class MapGenerator : MonoBehaviour
    {
        public Transform waterPrefab;
        public Transform grassPrefab;
        public Transform desertPrefab;
        public Transform mountainPrefab;
        public Transform forestPrefab;
        public Transform knightPrefab;

        private Tile[,] tiles;

        public void GenerateMap()
        {
            TileGenerator tileGenerator = new TileGenerator(waterPrefab, grassPrefab, desertPrefab, mountainPrefab, forestPrefab);
            Transform[,] tileTransforms = tileGenerator.GenerateTiles();
            tiles = new Tile[tileTransforms.GetLength(0), tileTransforms.GetLength(1)];

            // destroy/create container object
            Transform mapHolder = RestoreMapholder();

            // create the tiles
            for (int x = 0; x < Constants.MapSettings.mapSize.x; x++)
            {
                for (int y = 0; y < Constants.MapSettings.mapSize.y; y++)
                {
                    Vector3 tilePosition = TileUtil.CoordToPosition(x, y);
                    Transform newTile = Instantiate(tileTransforms[x, y], tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                    newTile.localScale = Vector3.one * Constants.MapSettings.tileSize;
                    newTile.Rotate(Vector3.back * GenerateDegree());
                    newTile.parent = mapHolder;
                    newTile.name = "X:" + x + " Y:" + y + " " + newTile.tag;

                    // store created tile for later access
                    tiles[x, y] = newTile.GetComponent<Tile>();

                    // set properties of tile
                    tiles[x, y].Type = GetType(tileTransforms[x, y]);
                    tiles[x, y].xPos = x;
                    tiles[x, y].yPos = y;
                }
            }
        }

        private UnitType GetType(Transform tile)
        {
            if (tile == grassPrefab)
            {
                return UnitType.GrassTile;
            }
            else if (tile == desertPrefab)
            {
                return UnitType.DesertTile;
            }
            else if (tile == waterPrefab)
            {
                return UnitType.WaterTile;
            }
            else if (tile == mountainPrefab)
            {
                return UnitType.MountainTile;
            }
            else if (tile == forestPrefab)
            {
                return UnitType.ForestTile;
            }
            throw new InvalidOperationException();
        }

        private int GenerateDegree()
        {
            int[] angles = new int[]{ 0, 90, 180, 270, 360 };
            return angles[UnityEngine.Random.Range(0, 4)];
        }

        public Tile GetTileAt(Coord coord)
        {
            return tiles[coord.x, coord.y];
        }


        private Transform RestoreMapholder()
        {
            string holderName = "Generated Map";
            if (transform.Find(holderName))
            {
                DestroyImmediate(transform.Find(holderName).gameObject);
            }
            Transform mapHolder = new GameObject(holderName).transform;
            mapHolder.parent = transform;
            return mapHolder;
        }
    }
}