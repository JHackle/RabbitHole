namespace Hackle.Map
{
    [System.Serializable]
    public class MapSettings
    {
        public Coord MapSize = new Coord();
        public int TileSize = 1;
        public int Seed = 0;
        public float WaterPercent = 0;
        public float GrassPercent = 0;
        public float DesertPercent = 0;
        public float MountainPercent = 0;
        public float ForestPercent = 0;


        public float CalculateTilesSum()
        {
            return WaterPercent + GrassPercent + DesertPercent + MountainPercent + ForestPercent;
        }

        public float MinX()
        {
            return -MapSize.X / 2f * TileSize;
        }

        public float MaxX()
        {
            return MapSize.X / 2f * TileSize;
        }

        public float MinZ()
        {
            return -MapSize.Y / 2f * TileSize;
        }

        public float MaxZ()
        {
            return MapSize.Y / 2f * TileSize;
        }
    }
}