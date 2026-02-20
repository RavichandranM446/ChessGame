using UnityEngine;

public class CameraLock : MonoBehaviour
{
    void Update()
    {
        // Lock position
        transform.position = new Vector3(4f, 10f, -8f);

        // Lock rotation
        transform.rotation = Quaternion.Euler(60f, 0f, 0f);
    }
}
