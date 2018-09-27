namespace Hackle.Managers
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class InputManager : MonoBehaviour
    {
        public GameManager GameManager;

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
                    GameManager.Click(go);
                }
            }

            // handle the back button (ESC)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}