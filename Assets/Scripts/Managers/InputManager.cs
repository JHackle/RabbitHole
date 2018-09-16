namespace Hackle.Managers
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class InputManager : MonoBehaviour
    {
        public GameManager gameManager;
        public GameObject hud;

        void Update()
        {
            // handle mouse cursor
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                // react if the mous button was clicked
                if (Input.GetMouseButtonDown(0))
                {
                    // if we hover over an UI element we don't need raycasts
                    if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                    {
                        return;
                    }

                    // decide which object was clicked
                    GameObject go = hitInfo.collider.transform.parent.gameObject;
                    gameManager.Click(go);
                }
            }

            // handle the back button (ESC)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");
            }
        }

        public void NextRound()
        {
            gameManager.NextRound();
        }

        public void ShowBuildMenu()
        {
            GameObject buildMenu = hud.transform.Find("BuildMenu").gameObject;
            GameObject nextRound = hud.transform.Find("NextRound").gameObject;
            if (buildMenu.activeInHierarchy)
            {
                buildMenu.SetActive(false);
                nextRound.SetActive(true);
            }
            else
            {
                buildMenu.SetActive(true);
                nextRound.SetActive(false);
            }
        }
    }
}