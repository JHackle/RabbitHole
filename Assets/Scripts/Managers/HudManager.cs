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

        public Image Lumberjack;

        private Text nextRoundNumber;
        private Image nextRoundArrow;
        private Text woodCount;
        private Text foodCount;
        private Text goldCount;

        private void Start()
        {
            nextRoundNumber = Round.transform.Find("RoundNumber").GetComponent<Text>();
            nextRoundArrow = NextRoundButton.transform.Find("Image").GetComponent<Image>();
            woodCount = Wood.Find("Text").GetComponent<Text>();
            foodCount = Food.Find("Text").GetComponent<Text>();
            goldCount = Gold.Find("Text").GetComponent<Text>();
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
                ISelectable selected = SelectionManager.SelectedUnit<ISelectable>();

                // write the type of selected unit to the menu
                Text menuTitle = BuildMenu.transform.Find("Text").gameObject.GetComponent<Text>();
                menuTitle.text = selected.Type.ToString();
            }
            ShowMenu(showMenu);
        }

        public void ShowMenu(bool show)
        {
            GameObject buildMenu = BuildMenu.gameObject;
            buildMenu.SetActive(show);
        }

        internal void GoToNextRound()
        {
            // reset arrow color
            nextRoundArrow.color = Color.white;

            // increase round number
            nextRoundNumber.text = (int.Parse(nextRoundNumber.text) + 1) + "";
        }

        internal void ShowRoundFinish()
        {
            nextRoundArrow.color = Color.green;
        }
    }
}
