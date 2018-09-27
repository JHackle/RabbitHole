namespace Hackle.Managers
{
    using Hackle.Objects;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// This class is responsible for creating and showing the build menu.
    /// </summary>
    public class BuildMenuManager : MonoBehaviour
    {
        public Transform BuildMenu;
        public Transform Hud;
        public SelectionManager SelectionManager;


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
            GameObject nextRound = Hud.transform.Find("NextRound").gameObject;
            if (show)
            {
                buildMenu.SetActive(true);
                nextRound.SetActive(false);
            }
            else
            {
                buildMenu.SetActive(false);
                nextRound.SetActive(true);
            }
        }
    }
}
