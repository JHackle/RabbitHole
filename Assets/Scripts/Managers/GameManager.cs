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
            HumanPlayer.AddUnit(knightTransform.gameObject.GetComponent<Unit>());

            knightTransform = Instantiate(KnightPrefab, TileUtil.CoordToPosition(5, 6), Quaternion.identity).transform;
            knightTransform.parent = mapHolder;
            knightTransform.GetComponent<Unit>().xPos = 5;
            knightTransform.GetComponent<Unit>().yPos = 6;
            HumanPlayer.AddUnit(knightTransform.gameObject.GetComponent<Unit>());

            Transform villageCenter = Instantiate(VillageCenterPrefab, TileUtil.CoordToPosition(5, 5), Quaternion.identity).transform;
            villageCenter.parent = mapHolder;
            villageCenter.GetComponent<Unit>().xPos = 5;
            villageCenter.GetComponent<Unit>().yPos = 5;
            HumanPlayer.AddUnit(villageCenter.gameObject.GetComponent<Unit>());
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
            Tile tile = go.GetComponent<Tile>();
            ISelectable selectable = go.GetComponent<ISelectable>();
            if (tile != null)
            {
                SelectionManager.ClickTile(tile);
            }
            else if (selectable != null)
            {
                SelectionManager.Select(selectable);
                SelectionManager.UpdateMovableFields();
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