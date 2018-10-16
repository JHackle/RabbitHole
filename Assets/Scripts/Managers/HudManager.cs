namespace Hackle.Managers
{
    using Hackle.Factories;
    using Hackle.Objects;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// This class is responsible for creating and showing the HUD.
    /// </summary>
    public class HudManager : MonoBehaviour
    {
        public SelectionManager SelectionManager;
        public MenuItemFactory MenuItemFactory;

        public Transform MenuButton;
        public Transform Capacity;
        public Transform Wood;
        public Transform Gold;
        public Transform Food;
        public Transform Round;

        public Transform NextRoundButton;
        public Transform BuildMenu;

        private Text nextRoundNumber;
        private Image nextRoundArrow;
        private Text woodCount;
        private Text foodCount;
        private Text goldCount;
        private Text capacityCount;

        private Text menuTitle;
        private Transform buildMenuContent;
        private Text debug;

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
            menuTitle = BuildMenu.transform.Find("Text").gameObject.GetComponent<Text>();
            debug = BuildMenu.Find("Debug").GetComponent<Text>();
            buildMenuContent = BuildMenu.Find("ScrollView").Find("Viewport").Find("Content");

            // define colors
            ColorUtility.TryParseHtmlString("#1EFF00FF", out highlightColor);
            ColorUtility.TryParseHtmlString("#FED26CFF", out defaultColor);
            nextRoundArrow.color = defaultColor;
        }

        internal void Debug(string text)
        {
            debug.gameObject.SetActive(text != null);
            debug.text = text;
        }

        /// <summary>
        /// Automatically updates the build menu depending on the game state.
        /// </summary>
        internal void UpdateBuildMenu()
        {
            ClearBuildMenu();

            if (SelectionManager.IsUnitSelected())
            {
                // add updated content to the build menu
                ISelectable selected = SelectionManager.SelectedUnit<ISelectable>();
                menuTitle.text = selected.Type.ToString();
                MenuItemFactory.CreateMenuItems();
            }
        }

        internal void ChangeMaxCapacity(int cap)
        {
            maxCapacity += cap;
            UpdateCapacity();
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

        private void UpdateCapacity()
        {
            capacityCount.text = maxCapacity + "/" + capacity;
        }

        private void ClearBuildMenu()
        {
            Debug(null);
            // remove text from build menu
            menuTitle.text = "";
            // destroy all menu elements
            foreach (Transform child in buildMenuContent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}