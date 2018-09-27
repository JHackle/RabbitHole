namespace Hackle.Managers
{
    using Hackle.Map;
    using Hackle.Objects;
    using Hackle.Util;
    using System;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// This class is responsible for:
    ///  - all unit selection within the game
    ///  - highlighting tiles which are in range of an selected unit
    /// </summary>
    public class SelectionManager : MonoBehaviour
    {
        public MapGenerator MapGenerator;

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
            UpdateMovableFields();
        }

        /// <summary>
        /// This method updates the movable fields according to the currently selected unit.
        /// </summary>
        private void UpdateMovableFields()
        {
            DisHighlightTiles();

            // we only have to do something if a unit is selected, and it actually can move
            if ((currentSelection != null) && (currentSelection is IMovable) && ((currentSelection as IMovable).CanMove()))
            {
                HighlightTiles();
            }
        }

        private void DisHighlightTiles()
        {
            foreach (Coord tile in tilesToHighlight)
            {
                MapGenerator.GetTileAt(tile).Select(false);
            }
            tilesToHighlight.Clear();
        }

        private void HighlightTiles()
        {
            Coord knightPos = currentSelection.Position();
            IMovable movable = currentSelection as IMovable;

            // calculate borders of the map
            int xMin = 0;
            int xMax = Constants.MapSettings.MapSize.X;
            int yMin = 0;
            int yMax = Constants.MapSettings.MapSize.Y;

            // calculate reachable tiles
            int xStart = knightPos.X - movable.Steps;
            int xStop = knightPos.X + movable.Steps;
            int yStart = knightPos.Y - movable.Steps;
            int yStop = knightPos.Y + movable.Steps;

            // collect all tiles in range
            for (int x = xStart; x <= xStop; x++)
            {
                for (int y = yStart; y <= yStop; y++)
                {
                    // don't highlight the tile of the selected unit
                    if ((knightPos.X == x) && (knightPos.Y == y))
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
                MapGenerator.GetTileAt(tile).Select(true);
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