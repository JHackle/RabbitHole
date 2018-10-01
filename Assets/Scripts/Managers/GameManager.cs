namespace Hackle.Managers
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

        private PlayerManager humanPlayer;

        void Start()
        {
            humanPlayer = new PlayerManager();
            MapGenerator.GenerateMap();

            Coord knight1Pos = new Coord(5, 5);
            Knight knight1 = ObjectFactory.CreateKnight();
            humanPlayer.AddObject(knight1);
            MapGenerator.GetTileAt(knight1Pos).SetUnit(knight1);

            Coord knight2Pos = new Coord(5, 6);
            Knight knight2 = ObjectFactory.CreateKnight();
            humanPlayer.AddObject(knight2);
            MapGenerator.GetTileAt(knight2Pos).SetUnit(knight2);

            VillageCenter center = ObjectFactory.CreateVillageCenter();
            humanPlayer.AddObject(center);
            MapGenerator.GetTileAt(knight1Pos).SetBuilding(center);
        }

        public void NextRound()
        {
            SelectionManager.Deselect();

            // collect resources
            humanPlayer.Harvest();

            // restore steps of all units
            humanPlayer.ResetSteps();

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
            if (!humanPlayer.CanAnyUnitMove())
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