using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The target that the camera should follow
    public Transform target;

    //-- Tracking PLayer --//
    // The distance the camera should maintain from the target
    public float distance = 10.0f;
    // The speed at which the camera should follow the target
    public float followSpeed = 5.0f;
    
    //-- Zooming --//
    public float minZoom = 1.0f;
    public float maxZoom = 20f;
    public float zoomSpeed = 1.0f;
    
    //-- Reference to the Camera component of the Main Camera --//
    private Camera camera;

    void Start()
    {
        // Retrieve the Camera component of the camera and save it
        camera = GetComponent<Camera>();
    } 
    void Update()
    {
        // Get the value of the mouse scroll wheel
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        // Adjust the camera's orthographic size based on the scroll value
        var orthographicSize = camera.orthographicSize;
        orthographicSize -= scroll * zoomSpeed;
        camera.orthographicSize = orthographicSize;

        // Clamp the orthographic size to the specified minimum and maximum values
        camera.orthographicSize = Mathf.Clamp(orthographicSize, minZoom, maxZoom);
        
        // Follow the target's position, maintaining the specified distance from the target
        var newPosition = target.position + Vector3.back * distance;
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }
}