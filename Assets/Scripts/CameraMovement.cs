using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] float MinCameraMovement = 25f;

    public float MouseSensitivity = 100f;

    [SerializeField] Transform PlayerBody;

    public InputAction Controls;

    float xRotation = 0f;

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 lookDelta = Controls.ReadValue<Vector2>();

        //Gets the movement data from the mouse, times by sensitivity and dependent on framerate with delta time.
        float mouseX = lookDelta.x * MouseSensitivity * Time.deltaTime;
        float mouseY = lookDelta.y * MouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        //Clamp basically means it can't go any further down than -90f and not higher than MinCameraMovement
        xRotation = Mathf.Clamp(xRotation, -MinCameraMovement, MinCameraMovement);
        //Only rotates on the X axis to avoid rotating the player body with the camera.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        PlayerBody.Rotate(Vector3.up * mouseX);

        
    }
}