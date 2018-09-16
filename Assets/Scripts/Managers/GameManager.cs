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
        public MapGenerator mapGenerator;
        public SelectionManager selectionManager;

        public Text roundNumber;
        public Image nextRoundArrow;

        public Transform knightPrefab;

        private List<MovableUnit> movableUnits = new List<MovableUnit>();

        void Start()
        {
            mapGenerator.GenerateMap();

            Transform mapHolder = RestoreMapholder();

            Transform knightTransform = Instantiate(knightPrefab, TileUtil.CoordToPosition(5, 5), Quaternion.identity).transform;
            knightTransform.parent = mapHolder;
            knightTransform.GetComponent<Knight>().xPos = 5;
            knightTransform.GetComponent<Knight>().yPos = 5;
            movableUnits.Add(knightTransform.gameObject.GetComponent<Knight>());

            knightTransform = Instantiate(knightPrefab, TileUtil.CoordToPosition(5, 6), Quaternion.identity).transform;
            knightTransform.parent = mapHolder;
            knightTransform.GetComponent<Knight>().xPos = 5;
            knightTransform.GetComponent<Knight>().yPos = 6;
            movableUnits.Add(knightTransform.gameObject.GetComponent<Knight>());
        }

        public void NextRound()
        {
            selectionManager.Deselect();
            movableUnits.ForEach(u => u.RestoreSteps());

            // reset arrow color
            nextRoundArrow.color = Color.white;

            // increase round number
            roundNumber.text = (int.Parse(roundNumber.text) + 1) + "";
        }

        public void Click(GameObject go)
        {
            Tile tile = go.GetComponent<Tile>();
            ISelectable selectable = go.GetComponent<ISelectable>();
            if (tile != null)
            {
                selectionManager.ClickTile(tile);
            }
            else if (selectable != null)
            {
                selectionManager.Select(selectable);
                selectionManager.UpdateMovableFields();
            }

            // make the arrow green if no units can move any more
            bool canMove = false;
            movableUnits.ForEach(u => { canMove |= u.CanMove(); });
            if (!canMove)
            {
                nextRoundArrow.color = Color.green;
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