namespace Hackle.Map
{
    [System.Serializable]
    public class MapSettings
    {
        public Coord mapSize = new Coord();
        public int tileSize = 1;
        public int seed = 0;
        public float waterPercent = 0;
        public float grassPercent = 0;
        public float desertPercent = 0;
        public float mountainPercent = 0;
        public float forestPercent = 0;


        public float CalculateTilesSum()
        {
            return waterPercent + grassPercent + desertPercent + mountainPercent + forestPercent;
        }
    }
}