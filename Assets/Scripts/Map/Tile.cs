namespace Hackle.Map
{
    using Hackle.Objects;

    public class Tile : SelectableUnit
    {
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

        public void SetBuilding(ISelectable building)
        {
            this.building = building;
        }

        public void SetUnit(IMovable unit)
        {
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