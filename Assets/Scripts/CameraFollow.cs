using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The target that the camera should follow
    public Transform target;

    // The distance the camera should maintain from the target
    public float distance = 10.0f;

    // The speed at which the camera should follow the target
    public float followSpeed = 5.0f;

    void Update()
    {
        // Follow the target's position, maintaining the specified distance from the target
        Vector3 newPosition = target.position + Vector3.back * distance;
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
}