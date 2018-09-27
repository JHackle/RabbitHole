namespace Hackle.Util
{
    using Hackle.Map;
    using UnityEngine;

    public class TileUtil
    {
        public static Vector3 CoordToPosition(Coord coord)
        {
            return CoordToPosition(coord.X, coord.Y);
        }

        public static Vector3 CoordToPosition(int x, int y)
        {
            MapSettings mapSettings = Constants.MapSettings;
            int tileSize = mapSettings.TileSize;
            Coord mapSize = mapSettings.MapSize;

            if ((x < 0) || (x >= mapSize.X) || (y < 0) || (y > mapSize.Y))
            {
                throw new CoordinateTranslationException("Coordinates are not in range: " + x + ":" + y);
            }

            return new Vector3(-mapSize.X / 2f + 0.5f * tileSize + x * tileSize, 0, -mapSize.Y / 2f + 0.5f * tileSize + y * tileSize);
        }

        public static Coord RandomPosition()
        {
            Coord size = Constants.MapSettings.MapSize;
            int randX = Random.Range(0, size.X);
            int randY = Random.Range(0, size.Y);
            return new Coord(randX, randY);
        }
    }
}