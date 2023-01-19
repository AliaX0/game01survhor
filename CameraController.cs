using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  
    public Transform target; // Creates a Transform object to manipulate the camera's position with the player as the target.
    public Vector3 offset; // The camera offset from the player
    private float currentZoom = 2f; // How zoomed in the camera is in accordance to the target (player).
    public float pitch = 2f; // The height of our character, pitch meaning how high or low we are
    public float zoomSpeed = 4f; // How fast the zoom is
    public float minZoom = 5f; // mininum zoom
    public float maxZoom = 15f; // maximum zoom
    public float yawSpeed = 100f; // Yaw meaning turning left and right
    private float currentYaw = 0f;




    void Update(){
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; // By default it is inverted so we use -=, and we are retrieving the information for the zoom from the mousewheel.
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom); // This limits the min, max values for the zoom on the values we set earlier, so that the player can't zoom in or out too much.
        currentYaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime; // Checking if we use our horizontal axis keys, (a,d,<,>, or joystick) and after taking in the yawSpeed and time, we store it in a currentYaw variable.
    }

    void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom; // Placing the camera in a certain position away from the target
        transform.LookAt(target.position + Vector3.up * pitch); 

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
