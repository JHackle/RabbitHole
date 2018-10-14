namespace Hackle.Managers
{
    using Hackle.Map;
    using Hackle.Objects;
    using Hackle.Objects.Units;
    using UnityEngine;
    using UnityEngine.EventSystems;
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
                    if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null)
                    {
                        return;
                    }

                    // decide which object was clicked
                    GameObject go = hitInfo.collider.transform.parent.gameObject;
                    GameManager.Click(go);
                }
                // just print some debug output in case of a left click
                else if (Input.GetMouseButtonDown(1))
                {
                    // decide which object was clicked
                    IObject go = hitInfo.collider.transform.parent.gameObject.GetComponent<IObject>();
                    string position = go.Tile != null ? go.Tile.Position.ToString() : (go as Tile).Position.ToString();
                    Debug.Log("-----------------------------");
                    Debug.Log(go.Type + " (" + position + ")");

                    if (go is Tile)
                    {
                        Tile tile = go as Tile;
                        Debug.Log("Has Unit: " + tile.HasUnit());
                        Debug.Log("Has Building: " + tile.HasBuilding());
                        Debug.Log("Is Selected: " + tile.IsSelected());
                    }
                    else if (go is Building)
                    {
                        Building building = go as Building;
                        Debug.Log("Produces Capacity: " + building.Capacity);
                        Debug.Log("Produces Resources: " + building.ResourcesPerTurn);
                    }
                    else if (go is Unit)
                    {
                        Unit unit = go as Unit;
                        Debug.Log("Uses Capacity: " + unit.CapacityValue);
                    }
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