﻿namespace Hackle.Managers
{
    using Hackle.Factories;
    using Hackle.Map;
    using Hackle.Objects;
    using Hackle.Objects.Buildings;
    using Hackle.Objects.Units;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public MapGenerator MapGenerator;
        public SelectionManager SelectionManager;
        public HudManager HudManager;
        public ObjectFactory ObjectFactory;
        public CameraManager CameraManager;
        public PlayerManager HumanPlayer;

        void Start()
        {
            MapGenerator.GenerateMap();

            // set starting resources
            HumanPlayer.Wood = 20;
            HumanPlayer.Food = 0;
            HumanPlayer.Gold = 0;

            Coord startingPosition = MapGenerator.GetStartingPosition();

            Knight knight = ObjectFactory.CreateKnight();
            HumanPlayer.AddObject(knight);
            MapGenerator.GetTileAt(startingPosition).SetUnit(knight);

            VillageCenter center = ObjectFactory.CreateVillageCenter();
            HumanPlayer.AddObject(center);
            MapGenerator.GetTileAt(startingPosition).SetBuilding(center);

            // move camera to village center
            CameraManager.MoveCameraTo(center.transform.position);
        }

        public void NextRound()
        {
            SelectionManager.Deselect();

            // collect resources
            HumanPlayer.Harvest();

            // restore steps of all units
            HumanPlayer.ResetSteps();

            HudManager.GoToNextRound();
        }

        public void Click(GameObject go)
        {
            IObject clickedObject = go.GetComponent<IObject>();
            
            if (clickedObject is Tile)
            {
                HandleClickOnTile(clickedObject as Tile);
            }
            else 
            {
                // if the clicked object is not a tile, just select the clicked object and update the build menu
                SelectionManager.Select(clickedObject as ISelectable);
                HudManager.UpdateBuildMenu();
            }

            // indicate that no unit can move any more
            if (!HumanPlayer.CanAnyUnitMove())
            {
                HudManager.ShowRoundFinish();
            }
        }

        private void HandleClickOnTile(Tile tile)
        {
            // was a unit selected before?
            if (SelectionManager.IsSelectedUnitOfType<IMovable>())
            {
                // is the clicked tile empty and in range of selected unit?
                if (tile.IsSelected() && !tile.HasUnit())
                {
                    // just move the unit to the clicked tile
                    SelectionManager.SelectedUnit<IMovable>().Move(tile);
                }
                // remove the selection and hide build menu
                SelectionManager.Deselect();
                HudManager.UpdateBuildMenu();
            }
            else
            {
                // in any other case the clicked tile should be selected and the build menu updated
                SelectionManager.Select(tile as ISelectable);
                HudManager.UpdateBuildMenu();
            }
        }
    }
}

// click on tile
    // unit was selected before
        // clicked tile was in range of selected unit
            //- move selected unit
            //- remove selection
            //- update tile highlight
            //- update build menu
        // clicked tile was not in range of selected unit
            //- remove selection
            //- update tile highlight
            //- update build menu
    // tile was selected before
        //- select unit
        //- update build menu
    // building was selected before
        //- select unit
        //- update build menu
    // nothing was selected before
        //- select unit
        //- update build menu
// click on unit
    // unit was selected before
        //- select unit
        //- update build menu
        //- update tile highlight
    // tile was selected before
        //- select unit
        //- update build menu
        //- update tile highlight
    // building was selected before
        //- select unit
        //- update build menu
        //- update tile highlight
// click on building
    // unit was selected before
        //- select unit
        //- update build menu
        //- update tile highlight
    // tile was selected before
        //- select unit
        //- update build menu
    // building was selected before
        //- select unit
        //- update build menu




//GameManager
//- move selected unit

//SelectionMananger
//- select unit
//- remove selection
//- update tile highlight

//BuildMenuMananger
//- update build menu