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

            Building center = ObjectFactory.CreateBuilding(ObjectType.VillageCenter);
            HumanPlayer.AddObject(center);
            MapGenerator.GetTileAt(startingPosition).SetBuilding(center);

            // move camera to village center
            CameraManager.MoveCameraTo(center.transform.position);
        }

        internal void NextRound()
        {
            SelectionManager.Deselect();

            // collect resources
            HumanPlayer.Harvest();

            // restore steps of all units
            HumanPlayer.ResetSteps();

            HudManager.GoToNextRound();
        }

        internal void ClickMenuItem(GameObject sender)
        {
            ObjectType type = sender.GetComponent<Objects.Object>().Type;
            Debug.Log(type + " was clicked");

            // add building
            Building building = ObjectFactory.CreateBuilding(type);
            Tile tile = SelectionManager.SelectedUnit<Tile>();
            tile.SetBuilding(building);
            HumanPlayer.AddObject(building);

            // clear build menu
            SelectionManager.Deselect();
            HudManager.UpdateBuildMenu();
        }

        internal void Click(GameObject go)
        {
            IObject clickedObject = go.GetComponent<IObject>();

            // if a unit was selected before and a tile is clicked we have to handle movements
            if (SelectionManager.IsSelectedUnitOfType<IMovable>() && clickedObject is Tile)
            {
                DoMovementIfPossible(clickedObject);
                // remove the selection
                SelectionManager.Deselect();
            }
            // if nothing was selected before, or we don't click on a tile
            else 
            {
                // just select the clicked object
                SelectionManager.Select(clickedObject as ISelectable);
            }
            // show selection changes in the build menu
            HudManager.UpdateBuildMenu();

            // indicate that no unit can move any more
            if (!HumanPlayer.CanAnyUnitMove())
            {
                HudManager.ShowRoundFinish();
            }
        }

        private void DoMovementIfPossible(IObject clickedObject)
        {
            Tile tile = clickedObject as Tile;
            // is the clicked tile empty and in range of selected unit?
            if (tile.IsSelected() && !tile.HasUnit())
            {
                // just move the unit to the clicked tile
                SelectionManager.SelectedUnit<IMovable>().Move(tile);
            }
        }
    }
}
