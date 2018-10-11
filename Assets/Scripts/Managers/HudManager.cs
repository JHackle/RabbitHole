namespace Hackle.Managers
{
    using System;
    using Hackle.Objects;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// This class is responsible for creating and showing the HUD.
    /// </summary>
    public class HudManager : MonoBehaviour
    {
        public SelectionManager SelectionManager;

        public Transform MenuButton;
        public Transform Capacity;
        public Transform Wood;
        public Transform Gold;
        public Transform Food;
        public Transform Round;

        public Transform NextRoundButton;
        public Transform BuildMenu;

        public GameObject MenuItem;

        public Sprite Lumberjack;
        public Sprite GoldMine;

        private Text nextRoundNumber;
        private Image nextRoundArrow;
        private Text woodCount;
        private Text foodCount;
        private Text goldCount;
        private Text capacityCount;

        private int maxCapacity = 0;
        private int capacity = 0;

        private Color defaultColor;
        private Color highlightColor;

        private void Start()
        {
            nextRoundNumber = Round.transform.Find("RoundNumber").GetComponent<Text>();
            nextRoundArrow = NextRoundButton.transform.Find("Image").GetComponent<Image>();
            woodCount = Wood.Find("Text").GetComponent<Text>();
            foodCount = Food.Find("Text").GetComponent<Text>();
            goldCount = Gold.Find("Text").GetComponent<Text>();
            capacityCount = Capacity.Find("Text").GetComponent<Text>();

            // define colors
            ColorUtility.TryParseHtmlString("#1EFF00FF", out highlightColor);
            ColorUtility.TryParseHtmlString("#FED26CFF", out defaultColor);
            nextRoundArrow.color = defaultColor;
        }

        internal void ChangeMaxCapacity(int cap)
        {
            maxCapacity += cap;
            UpdateCapacity();
        }

        private void UpdateCapacity()
        {
            capacityCount.text = maxCapacity + "/" + capacity;
        }

        internal void ChangeCapacity(int cap)
        {
            capacity += cap;
            UpdateCapacity();
        }

        internal String GetWood()
        {
            return woodCount.text;
        }

        internal String GetFood()
        {
            return foodCount.text;
        }

        internal String GetGold()
        {
            return goldCount.text;
        }

        internal void SetWood(int wood)
        {
            woodCount.text = wood + "";
        }

        internal void SetFood(int food)
        {
            foodCount.text = food + "";
        }

        internal void SetGold(int gold)
        {
            goldCount.text = gold + "";
        }


        /// <summary>
        /// Automatically updates the build menu depending on the game state.
        /// </summary>
        public void UpdateBuildMenu()
        {
            bool showMenu = SelectionManager.IsUnitSelected();
            if (showMenu)
            {
                CreateBuildMenu();
            }
            ShowMenu(showMenu);
        }

        private void CreateBuildMenu()
        {
            ISelectable selected = SelectionManager.SelectedUnit<ISelectable>();

            // write the type of selected unit to the menu
            Text menuTitle = BuildMenu.transform.Find("Text").gameObject.GetComponent<Text>();
            menuTitle.text = selected.Type.ToString();

            // destroy all menu elements first
            Transform content = BuildMenu.Find("ScrollView").Find("Viewport").Find("Content");
            foreach (Transform child in content)
            {
                GameObject.Destroy(child.gameObject);
            }

            GameObject item;
            switch (selected.Type)
            {
                case ObjectType.GrassTile:
                    break;
                case ObjectType.DesertTile:
                    break;
                case ObjectType.MountainTile:
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = GoldMine;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = Lumberjack;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = GoldMine;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = Lumberjack;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = GoldMine;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = Lumberjack;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = GoldMine;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = Lumberjack;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = GoldMine;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = Lumberjack;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = GoldMine;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = Lumberjack;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = GoldMine;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = Lumberjack;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = GoldMine;
                    item.transform.SetParent(content.transform, false);
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = Lumberjack;
                    item.transform.SetParent(content.transform, false);
                    break;
                case ObjectType.WaterTile:
                    break;
                case ObjectType.ForestTile:
                    item = Instantiate(MenuItem);
                    item.transform.Find("Image").GetComponent<Image>().sprite = Lumberjack;
                    item.transform.SetParent(content.transform, false);
                    break;
                case ObjectType.Knight:
                    break;
                case ObjectType.VillageCenter:
                    break;
            }
        }

        public void ShowMenu(bool show)
        {
            BuildMenu.gameObject.SetActive(show);
            NextRoundButton.gameObject.SetActive(!show);
        }

        internal void GoToNextRound()
        {
            // reset arrow color
            nextRoundArrow.color = defaultColor;

            // increase round number
            nextRoundNumber.text = (int.Parse(nextRoundNumber.text) + 1) + "";
        }

        internal void ShowRoundFinish()
        {
            nextRoundArrow.color = highlightColor;
        }
    }
}
