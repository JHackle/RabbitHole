namespace Hackle.Objects
{
    using Hackle.Map;

    public interface IObject
    {
        /// <summary>
        /// The type of the unit.
        /// </summary>
        ObjectType Type { get; set; }

        /// <summary>
        /// The tile based position of the unit.
        /// </summary>
        Coord Position { get; set; }
    }
}