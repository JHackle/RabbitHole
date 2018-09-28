namespace Hackle.Objects
{
    using Hackle.Map;
    using UnityEngine;

    /// <summary>
    /// Base class for alle objects in the game. Each Unit must have a tile based x/y position.
    /// </summary>
    public class Unit : MonoBehaviour, IUnit
    {
        public Coord Position { get; set; }

        public UnitType Type { get; set; }
    }
}