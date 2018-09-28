namespace Hackle.Managers
{
    using Hackle.Map;
    using Hackle.Objects;
    using Hackle.Util;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameManager : MonoBehaviour
    {
        public MapGenerator MapGenerator;
        public SelectionManager SelectionManager;
        public BuildMenuManager BuildMenuManager;

        public Text RoundNumber;
        public Image NextRoundArrow;

        public Transform VillageCenterPrefab;
        public Transform KnightPrefab;

        private PlayerManager humanPlayer;
        private Transform mapHolder;

        void Start()
        {
            mapHolder = RestoreMapholder();
            humanPlayer = new PlayerManager();
            MapGenerator.GenerateMap();

            Coord knight1Pos = new Coord(5, 5);
            Knight knight1 = CreateKnight(knight1Pos);
            humanPlayer.AddUnit(knight1);
            MapGenerator.GetTileAt(knight1Pos).SetUnit(knight1);

            Coord knight2Pos = new Coord(5, 6);
            Knight knight2 = CreateKnight(knight2Pos);
            humanPlayer.AddUnit(knight2);
            MapGenerator.GetTileAt(knight2Pos).SetUnit(knight2);

            VillageCenter center = CreateVillageCenter(knight1Pos);
            humanPlayer.AddUnit(center);
            MapGenerator.GetTileAt(knight1Pos).SetBuilding(center);
        }

        private VillageCenter CreateVillageCenter(Coord position)
        {
            Transform villageCenter = Instantiate(VillageCenterPrefab, TileUtil.CoordToPosition(5, 5), Quaternion.identity).transform;
            villageCenter.parent = mapHolder;
            villageCenter.GetComponent<Unit>().Position = position;
            VillageCenter center = villageCenter.gameObject.GetComponent<VillageCenter>();
            center.Type = UnitType.VillageCenter;
            return center;
        }

        private Knight CreateKnight(Coord position)
        {
            Transform knightTransform = Instantiate(KnightPrefab, TileUtil.CoordToPosition(position), Quaternion.identity).transform;
            knightTransform.parent = mapHolder;
            knightTransform.GetComponent<Unit>().Position = position;
            Knight knight = knightTransform.gameObject.GetComponent<Knight>();
            knight.Type = UnitType.Knight;
            return knight;
        }

        public void NextRound()
        {
            SelectionManager.Deselect();
            humanPlayer.ResetSteps();

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
                HandleClickOnTile(clickedUnit as Tile);
            }
            else 
            {
                // if the clicked object is not a tile, just select the clicked object and update the build menu
                SelectionManager.Select(clickedUnit as ISelectable);
                BuildMenuManager.UpdateBuildMenu();
            }

            // make the arrow green if no units can move any more
            if (!humanPlayer.CanAnyUnitMove())
            {
                NextRoundArrow.color = Color.green;
            }
        }

        private void HandleClickOnTile(Tile tile)
        {
            // was a unit selected before?
            if (SelectionManager.IsSelectedUnitOfType<IMovable>())
            {
                // is the clicked tile in range of selected unit?
                if (tile.IsSelected())
                {
                    // just move the unit to the clicked tile
                    SelectionManager.SelectedUnit<IMovable>().Move(tile.Position);
                }
                // remove the selection and hide build menu
                SelectionManager.Deselect();
                BuildMenuManager.UpdateBuildMenu();
            }
            else
            {
                // in any other case the clicked tile should be selected and the build menu updated
                SelectionManager.Select(tile as ISelectable);
                BuildMenuManager.UpdateBuildMenu();
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