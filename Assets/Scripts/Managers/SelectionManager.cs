namespace Hackle.Managers
{
    using Hackle.Map;
    using Hackle.Objects;
    using Hackle.Util;
    using System;
    using System.Collections;
    using UnityEngine;

    public class SelectionManager : MonoBehaviour
    {
        public MapGenerator mapGenerator;

        private ISelectable currentSelection;
        private Queue tilesToHighlight = new Queue();

        /// <summary>
        /// Selects the given unit. This may lead to different behaviour depending on the current state.
        /// If nothing was selected before: selects the given unit
        /// If the given unit was already selected: deselects the given unit
        /// If another unit was selected before: removes the previous selection and selects the given unit
        /// </summary>
        /// <param name="objectToSelect">The unit to select. May not be null.</param>
        public void Select(ISelectable objectToSelect)
        {
            // check if object can actually be select
            if (objectToSelect == null)
            {
                throw new InvalidOperationException("Unable to select null");
            }

            // if nothing was selected before, just select it
            if (currentSelection == null)
            {
                objectToSelect.Select(true);
                currentSelection = objectToSelect;
            }
            // if this object was already selected just deselect it
            else if (currentSelection.Equals(objectToSelect))
            {
                currentSelection.Select(false);
                currentSelection = null;
            }
            else
            {
                // deselect previous selected object
                currentSelection.Select(false);
                // select the new object
                currentSelection = objectToSelect;
                currentSelection.Select(true);
            }
        }

        /// <summary>
        /// This method updates the movable fields according to the currently selected unit.
        /// </summary>
        public void UpdateMovableFields()
        {
            DisHighlightTiles();

            // we only have to do something if a unit is selected, and it actually can move
            if ((currentSelection != null) && (currentSelection is IMovable) && ((currentSelection as IMovable).CanMove()))
            {
                HighlightTiles();
            }
        }

        internal void ClickTile(Tile tile)
        {
            // if a unit was selected previously and we clicked a tile in range (which was highlighted) then start a movement
            if (IsUnitSelected() && IsSelectedUnitOfType<IMovable>() && tile.IsSelected())
            {
                SelectedUnit<IMovable>().Move(tile.Position());
            }
            Deselect();
        }

        private void DisHighlightTiles()
        {
            foreach (Coord tile in tilesToHighlight)
            {
                mapGenerator.GetTileAt(tile).Select(false);
            }
            tilesToHighlight.Clear();
        }

        private void HighlightTiles()
        {
            Coord knightPos = currentSelection.Position();
            IMovable movable = currentSelection as IMovable;

            // calculate borders of the map
            int xMin = 0;
            int xMax = Constants.MapSettings.mapSize.x;
            int yMin = 0;
            int yMax = Constants.MapSettings.mapSize.y;

            // calculate reachable tiles
            int xStart = knightPos.x - movable.Steps;
            int xStop = knightPos.x + movable.Steps;
            int yStart = knightPos.y - movable.Steps;
            int yStop = knightPos.y + movable.Steps;

            // collect all tiles in range
            for (int x = xStart; x <= xStop; x++)
            {
                for (int y = yStart; y <= yStop; y++)
                {
                    // don't highlight the tile of the selected unit
                    if ((knightPos.x == x) && (knightPos.y == y))
                    {
                        continue;
                    }

                    // ignore tiles which are beyond the map borders
                    if ((x < xMin) || (x >= xMax) || (y < yMin) || (y >= yMax))
                    {
                        continue;
                    }

                    tilesToHighlight.Enqueue(new Coord(x, y));
                }
            }

            foreach (Coord tile in tilesToHighlight)
            {
                mapGenerator.GetTileAt(tile).Select(true);
            }
        }

        internal bool IsUnitSelected()
        {
            return currentSelection != null;
        }

        internal bool IsSelectedUnitOfType<T>()
        {
            return currentSelection is T;
        }

        internal T SelectedUnit<T>()
        {
            return (T)currentSelection;
        }

        internal void Deselect()
        {
            if (currentSelection != null)
            {
                currentSelection.Select(false);
                currentSelection = null;
            }
            DisHighlightTiles();
        }
    }
}