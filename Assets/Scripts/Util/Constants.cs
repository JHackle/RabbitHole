namespace Hackle.Util
{
    using Hackle.Map;

    public class Constants
    {
        public static int EnemyCount { get; set; }

        public static int EnemyStrength { get; set; }

        public static string PlayerColor { get; set; }

        public static MapSettings MapSettings { get; set; }

        static Constants()
        {
            // specify default values to avoid nullpointer exceptions
            // this might be important if the Game scene was started directly
            MapSettings = new MapSettings();
            MapSettings.MapSize = new Coord(10, 10);
            MapSettings.GrassPercent = 1;
            MapSettings.ForestPercent = 1;
            MapSettings.DesertPercent = 1;
            MapSettings.WaterPercent = 1;
            MapSettings.MountainPercent = 1;
        }
    }
}