using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 5f;
    public float zoomSpeed = 20f;
    public float panSpeed = 20f;

    private Vector3 dragOrigin;

    void Update()
    {
        RotateCamera();
        ZoomCamera();
        PanCamera();
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(1)) // Right mouse drag
        {
            float rotX = Input.GetAxis("Mouse X") * rotateSpeed;
            transform.RotateAround(Vector3.zero, Vector3.up, rotX);
        }
    }

    void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            transform.position += transform.forward * scroll * zoomSpeed;
        }
    }

    void PanCamera()
    {
        if (Input.GetMouseButtonDown(2))
            dragOrigin = Input.mousePosition;

        if (Input.GetMouseButton(2)) // Middle mouse drag
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(dragOrigin)
                               - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position += difference * panSpeed;
            dragOrigin = Input.mousePosition;
        }
    }
}
