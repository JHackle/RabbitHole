namespace Hackle.Managers
{
    using Hackle.Map;
    using Hackle.Util;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class MenuManager : MonoBehaviour {

        public GameObject ConfigMenu;
        public GameObject MainMenu;
        public GameObject SettingsMenu;

        public void Start()
        {
            ShowMainMenu();
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ShowConfigMenu()
        {
            MainMenu.SetActive(false);
            ConfigMenu.SetActive(true);
            SettingsMenu.SetActive(false);
        }

        public void ShowMainMenu()
        {
            MainMenu.SetActive(true);
            ConfigMenu.SetActive(false);
            SettingsMenu.SetActive(false);
        }

        public void ShowSettingsMenu()
        {
            MainMenu.SetActive(false);
            ConfigMenu.SetActive(false);
            SettingsMenu.SetActive(true);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void SaveData()
        {
            // save the number of enemies
            int enemyCount = GameObject.Find("EnemyCountCombo").GetComponent<Dropdown>().value + 1;
            Constants.EnemyCount = enemyCount;

            // save the strength of enemies
            int strength = GameObject.Find("EnemyStrengthCombo").GetComponent<Dropdown>().value + 1;
            Constants.EnemyStrength = strength;

            // save the world size
            int mapSize = GameObject.Find("WorldSizeCombo").GetComponent<Dropdown>().value;
            mapSize = (mapSize + 1) * 10;
            Constants.MapSettings.mapSize = new Coord(mapSize, mapSize);

            // save the player color
            var playerColorDropDown = GameObject.Find("ColorCombo").GetComponent<Dropdown>();
            string playerColor = playerColorDropDown.captionText.text;
            Constants.PlayerColor = playerColor;
        }

        private void Update()
        {
            // handle the back button
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitGame();
            }
        }
    }
}