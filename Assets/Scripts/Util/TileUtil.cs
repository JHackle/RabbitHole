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
            return new Vector3(-mapSettings.mapSize.x / 2f + 0.5f + x, 0, -mapSettings.mapSize.y / 2f + 0.5f + y) * mapSettings.tileSize;
        }
    }
}