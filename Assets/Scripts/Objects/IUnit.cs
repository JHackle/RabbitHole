namespace Hackle.Objects
{
    using Hackle.Map;

    public interface IUnit
    {
        /// <summary>
        /// The type of the unit.
        /// </summary>
        UnitType Type { get; set; }

        /// <summary>
        /// The tile based position of the unit.
        /// </summary>
        Coord Position { get; set; }
    }
}