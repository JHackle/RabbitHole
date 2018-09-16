namespace Hackle.Util
{
    using Hackle.Map;
    using UnityEngine;

    public class TileUtil
    {
        public static Vector3 CoordToPosition(Coord coord)
        {
            return CoordToPosition(coord.x, coord.y);
        }

        public static Vector3 CoordToPosition(int x, int y)
        {
            MapSettings mapSettings = Constants.MapSettings;
            int tileSize = mapSettings.tileSize;
            Coord mapSize = mapSettings.mapSize;

            if ((x < 0) || (x >= mapSize.x) || (y < 0) || (y > mapSize.y))
            {
                throw new CoordinateTranslationException("Coordinates are not in range: " + x + ":" + y);
            }

            return new Vector3(-mapSize.x / 2f + 0.5f * tileSize + x * tileSize, 0, -mapSize.y / 2f + 0.5f * tileSize + y * tileSize);
        }

        public static Coord RandomPosition()
        {
            Coord size = Constants.MapSettings.mapSize;
            int randX = Random.Range(0, size.x);
            int randY = Random.Range(0, size.y);
            return new Coord(randX, randY);
        }
    }
}