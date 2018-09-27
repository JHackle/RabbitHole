﻿namespace Hackle.Objects
{
    using Hackle.Map;

    public interface IUnit
    {
        /// <summary>
        /// The type of the unit.
        /// </summary>
        UnitType Type { get; set; }

        /// <summary>
        /// Returns the tile based position of the unit.
        /// </summary>
        /// <returns>new Coord object which represents the position of this unit</returns>
        Coord Position();
    }
}