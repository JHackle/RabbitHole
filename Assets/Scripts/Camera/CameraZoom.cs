namespace Hackle.Camera
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class CameraZoom : MonoBehaviour
    {
        private static readonly float PanSpeed = 10f;
        private static readonly float ZoomSpeedTouch = .05f;
        private static readonly float ZoomSpeedMouse = 20f;

        private static float[] BoundsX = new float[] { Util.Constants.MapSettings.MinX(), Util.Constants.MapSettings.MaxX() };
        private static float[] BoundsZ = new float[] { Util.Constants.MapSettings.MinZ() - 7f, Util.Constants.MapSettings.MaxZ() - 10f };
        private static readonly float[] ZoomBounds = new float[] { 10f, 50f };

        private Camera cam;

        private Vector3 lastPanPosition;
        private int panFingerId; // Touch mode only

        private bool wasZoomingLastFrame; // Touch mode only
        private Vector2[] lastZoomPositions; // Touch mode only

        void Awake()
        {
            cam = GetComponent<Camera>();
            BoundsX = new float[] { Util.Constants.MapSettings.MinX(), Util.Constants.MapSettings.MaxX() };
            BoundsZ = new float[] { Util.Constants.MapSettings.MinZ() - 7f, Util.Constants.MapSettings.MaxZ() - 10f };
        }

        void Update()
        {
            // if we hover over an UI element we don't need raycasts
            if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }

            Debug.DrawLine(new Vector3(BoundsX[0], 0, BoundsZ[0]), new Vector3(BoundsX[0], 0, BoundsZ[1]), Color.red);
            Debug.DrawLine(new Vector3(BoundsX[0], 0, BoundsZ[1]), new Vector3(BoundsX[1], 0, BoundsZ[1]), Color.red);
            Debug.DrawLine(new Vector3(BoundsX[1], 0, BoundsZ[1]), new Vector3(BoundsX[1], 0, BoundsZ[0]), Color.red);
            Debug.DrawLine(new Vector3(BoundsX[1], 0, BoundsZ[0]), new Vector3(BoundsX[0], 0, BoundsZ[0]), Color.red);

            if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
            {
                HandleTouch();
            }
            else
            {
                HandleMouse();
            }
        }

        public void MoveCameraTo(Vector3 target)
        {
            // calculate the correct target position first
            // this depends on how the camera is rotated, y position will never be changed
            Vector3 correctTarget = new Vector3(target.x, cam.transform.position.y, target.z - 10f);
            cam.transform.position = (correctTarget);
        }

        void HandleTouch()
        {
            switch (Input.touchCount)
            {

                case 1: // Panning
                    wasZoomingLastFrame = false;

                    // If the touch began, capture its position and its finger ID.
                    // Otherwise, if the finger ID of the touch doesn't match, skip it.
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        lastPanPosition = touch.position;
                        panFingerId = touch.fingerId;
                    }
                    else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                    {
                        PanCamera(touch.position);
                    }
                    break;

                case 2: // Zooming
                    Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                    if (!wasZoomingLastFrame)
                    {
                        lastZoomPositions = newPositions;
                        wasZoomingLastFrame = true;
                    }
                    else
                    {
                        // Zoom based on the distance between the new positions compared to the 
                        // distance between the previous positions.
                        float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                        float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                        float offset = newDistance - oldDistance;

                        ZoomCamera(offset, ZoomSpeedTouch);

                        lastZoomPositions = newPositions;
                    }
                    break;

                default:
                    wasZoomingLastFrame = false;
                    break;
            }
        }

        void HandleMouse()
        {
            // On mouse down, capture it's position.
            // Otherwise, if the mouse is still down, pan the camera.
            if (Input.GetMouseButtonDown(0))
            {
                lastPanPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                PanCamera(Input.mousePosition);
            }

            // Check for scrolling to zoom the camera
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            ZoomCamera(scroll, ZoomSpeedMouse);
        }

        void PanCamera(Vector3 newPanPosition)
        {
            // Determine how much to move the camera
            Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
            Vector3 move = new Vector3(offset.x * PanSpeed, 0, offset.y * PanSpeed);

            // Perform the movement
            cam.transform.Translate(move, Space.World);

            // Ensure the camera remains within bounds.
            Vector3 pos = cam.transform.position;
            pos.x = Mathf.Clamp(cam.transform.position.x, BoundsX[0], BoundsX[1]);
            pos.z = Mathf.Clamp(cam.transform.position.z, BoundsZ[0], BoundsZ[1]);
            cam.transform.position = pos;

            // Cache the position
            lastPanPosition = newPanPosition;
        }

        void ZoomCamera(float offset, float speed)
        {
            if (offset == 0)
            {
                return;
            }

            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
        }
    }
}
