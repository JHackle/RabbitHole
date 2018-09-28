namespace Hackle.Objects
{
    using Hackle.Map;
    using UnityEngine;

    /// <summary>
    /// Base class for all objects in the game. Each object must have a tile based x/y position.
    /// </summary>
    public class Object : MonoBehaviour, IObject
    {
        public Coord Position { get; set; }

        public UnitType Type { get; set; }
    }
}