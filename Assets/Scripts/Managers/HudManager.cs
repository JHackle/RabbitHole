namespace Hackle.Managers
{
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

        private Text nextRoundNumber;
        private Image nextRoundArrow;

        private void Start()
        {
            nextRoundNumber = Round.transform.Find("RoundNumber").GetComponent<Text>();
            nextRoundArrow = NextRoundButton.transform.Find("Image").GetComponent<Image>();
        }

        /// <summary>
        /// Automatically updates the build menu depending on the game state.
        /// </summary>
        public void UpdateBuildMenu()
        {
            bool showMenu = SelectionManager.IsUnitSelected();
            if (showMenu)
            {
                // write the type of selected unit to the menu
                ISelectable selected = SelectionManager.SelectedUnit<ISelectable>();
                Text menuTitle = BuildMenu.transform.Find("Text").gameObject.GetComponent<Text>();
                menuTitle.text = selected.Type.ToString();

                // TODO : fill menu with content
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
