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
        /// The tile where this object is placed on.
        /// </summary>
        Tile Location { get; set; }
    }
}