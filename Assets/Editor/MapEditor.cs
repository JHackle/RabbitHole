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
            Constants.MapSettings.mapSize = new Coord(10, 10);
            Constants.MapSettings.grassPercent = 1;
            Constants.MapSettings.forestPercent = 1;
            Constants.MapSettings.desertPercent = 1;
            Constants.MapSettings.waterPercent = 1;
            Constants.MapSettings.mountainPercent = 1;

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