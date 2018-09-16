namespace Hackle.Managers
{
    using Hackle.Map;
    using Hackle.Objects;
    using Hackle.Util;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameManager : MonoBehaviour
    {
        public MapGenerator mapGenerator;
        public SelectionManager selectionManager;

        public Text roundNumber;

        public Transform knightPrefab;

        private Knight knight;

        void Start()
        {
            mapGenerator.GenerateMap();

            Transform mapHolder = RestoreMapholder();
            Transform knightTransform = Instantiate(knightPrefab, TileUtil.CoordToPosition(5, 5), Quaternion.identity).transform;
            knightTransform.parent = mapHolder;
            knightTransform.GetComponent<Knight>().xPos = 5;
            knightTransform.GetComponent<Knight>().yPos = 5;
            knight = knightTransform.gameObject.GetComponent<Knight>();
        }

        public void NextRound()
        {
            selectionManager.Deselect();
            knight.RestoreSteps();

            // increase round number
            roundNumber.text = (int.Parse(roundNumber.text) + 1) + "";
        }

        public void Click(GameObject go)
        {
            Tile tile = go.GetComponent<Tile>();
            if (tile != null)
            {
                selectionManager.ClickTile(tile);
                return;
            }

            ISelectable selectable = go.GetComponent<ISelectable>();
            if (selectable != null)
            {
                selectionManager.Select(selectable);
                selectionManager.UpdateMovableFields();
                return;
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