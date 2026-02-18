using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5f;

    public float acceleration = 20f;
    public float deceleration = 15f;
    public float maxSpeed = 8f;

    public InputAction Controls;

    Vector2 moveDirection = Vector2.zero;

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

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Controls.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 move = transform.right * moveDirection.x + transform.forward * moveDirection.y;

        move.Normalize();

        Vector3 currentvelocity = rb.linearVelocity;
        Vector3 targetVelocity = move * maxSpeed;

        targetVelocity.y = currentvelocity.y;

        if (moveDirection != Vector2.zero)
        {

            rb.linearVelocity = Vector3.Lerp(currentvelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(currentvelocity, new Vector3(0, currentvelocity.y, 0), deceleration * Time.fixedDeltaTime);
        }
    }
}
