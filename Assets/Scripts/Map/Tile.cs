namespace Hackle.Map
{
    using System;
    using Hackle.Objects;
    using Hackle.Util;
    using UnityEngine;

    public class Tile : SelectableObject
    {
        public Coord Position { get; set; }

        private IMovable unit;
        private ISelectable building;

        public bool HasUnit()
        {
            return unit != null;
        }

        public bool HasBuilding()
        {
            return building != null;
        }

        public void SetBuilding(SelectableObject building)
        {
            // actually set the position of the building
            building.transform.position = TileUtil.CoordToPosition(Position);
            // set a link to this tile in the building
            building.Location = this;
            // store the building on this tile
            this.building = building;
        }

        public void SetUnit(MovableObject unit)
        {
            // actually set the position of the unit
            unit.transform.Translate(TileUtil.CoordToPosition(Position));
            // set a link to this tile in the building
            unit.Location = this;
            // store the building on this tile
            this.unit = unit;
        }

        public void RemoveBuilding()
        {
            building = null;
        }

        public void RemoveUnit()
        {
            unit = null;
        }
    }
}