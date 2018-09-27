namespace Hackle.Managers
{
    using Hackle.Map;
    using Hackle.Objects;
    using Hackle.Util;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameManager : MonoBehaviour
    {
        public MapGenerator MapGenerator;
        public SelectionManager SelectionManager;
        public BuildMenuManager BuildMenu;

        public Text RoundNumber;
        public Image NextRoundArrow;

        public Transform VillageCenterPrefab;
        public Transform KnightPrefab;

        private PlayerManager HumanPlayer;

        void Start()
        {
            HumanPlayer = new PlayerManager();
            MapGenerator.GenerateMap();

            Transform mapHolder = RestoreMapholder();

            Transform knightTransform = Instantiate(KnightPrefab, TileUtil.CoordToPosition(5, 5), Quaternion.identity).transform;
            knightTransform.parent = mapHolder;
            knightTransform.GetComponent<Unit>().xPos = 5;
            knightTransform.GetComponent<Unit>().yPos = 5;
            Knight knight1 = knightTransform.gameObject.GetComponent<Knight>();
            knight1.Type = UnitType.Knight;
            HumanPlayer.AddUnit(knight1);

            knightTransform = Instantiate(KnightPrefab, TileUtil.CoordToPosition(5, 6), Quaternion.identity).transform;
            knightTransform.parent = mapHolder;
            knightTransform.GetComponent<Unit>().xPos = 5;
            knightTransform.GetComponent<Unit>().yPos = 6;
            Knight knight2 = knightTransform.gameObject.GetComponent<Knight>();
            knight2.Type = UnitType.Knight;
            HumanPlayer.AddUnit(knight2);

            Transform villageCenter = Instantiate(VillageCenterPrefab, TileUtil.CoordToPosition(5, 5), Quaternion.identity).transform;
            villageCenter.parent = mapHolder;
            villageCenter.GetComponent<Unit>().xPos = 5;
            villageCenter.GetComponent<Unit>().yPos = 5;
            VillageCenter center = villageCenter.gameObject.GetComponent<VillageCenter>();
            center.Type = UnitType.VillageCenter;
            HumanPlayer.AddUnit(center);
        }

        public void NextRound()
        {
            SelectionManager.Deselect();
            HumanPlayer.ResetSteps();

            // reset arrow color
            NextRoundArrow.color = Color.white;

            // increase round number
            RoundNumber.text = (int.Parse(RoundNumber.text) + 1) + "";
        }

        public void Click(GameObject go)
        {
            IUnit clickedUnit = go.GetComponent<IUnit>();

            if (clickedUnit is Tile)
            {
                Tile tile = clickedUnit as Tile;
                if (SelectionManager.IsSelectedUnitOfType<IMovable>())
                {
                    if (tile.IsSelected())
                    {
                        SelectionManager.SelectedUnit<IMovable>().Move(tile.Position());
                    }
                    SelectionManager.Deselect();
                    BuildMenu.UpdateBuildMenu();
                }
                else
                {
                    SelectionManager.Select(clickedUnit as ISelectable);
                    BuildMenu.UpdateBuildMenu();
                }
            }
            else 
            {
                ISelectable selectable = clickedUnit as ISelectable;
                SelectionManager.Select(selectable);
                BuildMenu.UpdateBuildMenu();
            }

            // make the arrow green if no units can move any more
            if (!HumanPlayer.CanAnyUnitMove())
            {
                NextRoundArrow.color = Color.green;
            }
        }

        private Transform RestoreMapholder()
        {
            string holderName = "Game Objects";
            if (transform.Find(holderName))
            {
                DestroyImmediate(transform.Find(holderName).gameObject);
            }
            Transform mapHolder = new GameObject(holderName).transform;
            mapHolder.parent = transform;
            return mapHolder;
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