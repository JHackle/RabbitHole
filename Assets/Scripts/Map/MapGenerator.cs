namespace Hackle.Map
{
    using System;
    using Hackle.Objects;
    using Hackle.Util;
    using UnityEngine;

    public class MapGenerator : MonoBehaviour
    {
        public Transform WaterPrefab;
        public Transform GrassPrefab;
        public Transform DesertPrefab;
        public Transform MountainPrefab;
        public Transform ForestPrefab;
        public Transform KnightPrefab;

        private Tile[,] tiles;

        public void GenerateMap()
        {
            TileGenerator tileGenerator = new TileGenerator(WaterPrefab, GrassPrefab, DesertPrefab, MountainPrefab, ForestPrefab);
            Transform[,] tileTransforms = tileGenerator.GenerateTiles();
            tiles = new Tile[tileTransforms.GetLength(0), tileTransforms.GetLength(1)];

            // destroy/create container object
            Transform mapHolder = RestoreMapholder();

            // create the tiles
            for (int x = 0; x < Constants.MapSettings.MapSize.X; x++)
            {
                for (int y = 0; y < Constants.MapSettings.MapSize.Y; y++)
                {
                    Vector3 tilePosition = TileUtil.CoordToPosition(x, y);
                    Transform newTile = Instantiate(tileTransforms[x, y], tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                    newTile.localScale = Vector3.one * Constants.MapSettings.TileSize;
                    newTile.Rotate(Vector3.back * GenerateDegree());
                    newTile.parent = mapHolder;
                    newTile.name = "X:" + x + " Y:" + y + " " + newTile.tag;

                    // store created tile for later access
                    tiles[x, y] = newTile.GetComponent<Tile>();

                    // set properties of tile
                    tiles[x, y].Type = GetType(tileTransforms[x, y]);
                    tiles[x, y].X = x;
                    tiles[x, y].Y = y;
                }
            }
        }

        private UnitType GetType(Transform tile)
        {
            if (tile == GrassPrefab)
            {
                return UnitType.GrassTile;
            }
            else if (tile == DesertPrefab)
            {
                return UnitType.DesertTile;
            }
            else if (tile == WaterPrefab)
            {
                return UnitType.WaterTile;
            }
            else if (tile == MountainPrefab)
            {
                return UnitType.MountainTile;
            }
            else if (tile == ForestPrefab)
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
            return tiles[coord.X, coord.Y];
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