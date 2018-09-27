namespace Hackle.Objects
{
    using Hackle.Map;
    using UnityEngine;

    /// <summary>
    /// Base class for alle objects in the game. Each Unit must have a tile based x/y position.
    /// </summary>
    public class Unit : MonoBehaviour, IUnit
    {
        public int xPos;
        public int yPos;

        public Coord Position()
        {
            return new Coord(xPos, yPos);
        }

        public UnitType Type { get; set; }
    }
}