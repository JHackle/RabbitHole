namespace Hackle.Objects
{
    using Hackle.Map;
    using UnityEngine;

    /// <summary>
    /// Base class for alle objects in the game. Each Unit must have a tile based x/y position.
    /// </summary>
    public class Unit : MonoBehaviour, IUnit
    {
        public int X;
        public int Y;

        public Coord Position()
        {
            return new Coord(X, Y);
        }

        public UnitType Type { get; set; }
    }
}