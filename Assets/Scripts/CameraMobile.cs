using UnityEngine;

public class CameraMobile : MonoBehaviour
{
    public float zoomSpeed = 0.3f;
    public float moveSpeed = 0.02f;

    // MOBILE ZOOM LIMITS
    public float minZoom = 14f;
    public float maxZoom = 25f;

    // BOARD LIMITS
    public float minX = -3f;
    public float maxX = 10f;
    public float minZ = -3f;
    public float maxZ = 10f;

    private Camera cam;
    private Vector2 lastPanPosition;
    private int panFingerId;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // No touches ? exit
        if (Input.touchCount == 0)
            return;

        // ------ PINCH ZOOM ------
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            float prevDistance = (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;
            float currentDistance = (t0.position - t1.position).magnitude;

            float delta = currentDistance - prevDistance;

            cam.orthographicSize -= delta * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
            return;
        }

        // ------ DRAG MOVE ------
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastPanPosition = touch.position;
                panFingerId = touch.fingerId;
            }
            else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - lastPanPosition;

                Vector3 move = new Vector3(-delta.x * moveSpeed, 0, -delta.y * moveSpeed);
                transform.position += move;

                lastPanPosition = touch.position;

                // LIMIT THE CAMERA INSIDE BOARD
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, minX, maxX),
                    transform.position.y,
                    Mathf.Clamp(transform.position.z, minZ, maxZ)
                );
            }
        }
    }
}
