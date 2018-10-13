namespace Hackle.Editor
{
    using Hackle.Map;
    using Hackle.Util;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(MapGenerator))]
    [CanEditMultipleObjects]
    public class EditorScript : Editor
    {
        public override void OnInspectorGUI()
        {
            Constants.MapSettings = new MapSettings();
            Constants.MapSettings.MapSize = new Coord(20, 20);
            Constants.MapSettings.GrassPercent = 1;
            Constants.MapSettings.ForestPercent = 1;
            Constants.MapSettings.DesertPercent = 1;
            Constants.MapSettings.WaterPercent = 1;
            Constants.MapSettings.MountainPercent = 1;

            MapGenerator map = target as MapGenerator;
            if (DrawDefaultInspector())
            {
                map.GenerateMap();
            }

            if (GUILayout.Button("Generate Map"))
            {
                map.GenerateMap();
            }
        }
    }
}