using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float acceleration = 20f;
    public float deceleration = 15f;
    public float maxSpeed = 8f;

    [Header("Jumping")]
    public float JumpForce = 10f;
    public float DashForce = 10f;

    [Header("InputActions")]
    public InputAction JumpAction;
    public InputAction Controls;
    public InputAction Dash;

    bool IsGrounded;
    bool TryJump;
    bool TryDash;
    Vector2 moveDirection = Vector2.zero;

    private void OnEnable()
    {
        Controls.Enable();
        JumpAction.Enable();
        Dash.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
        JumpAction.Disable();
        Dash.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveDirection = Controls.ReadValue<Vector2>();

        if (JumpAction.triggered && IsGrounded)
        {
            TryJump = true;
        }
        if (Dash.triggered && !IsGrounded)
        {
            TryDash = true;
        }
    }

    private void FixedUpdate()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        Vector3 move = transform.right * moveDirection.x + transform.forward * moveDirection.y;
        move.Normalize();

        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = move * maxSpeed;
        targetVelocity.y = currentVelocity.y;

        if (TryJump && IsGrounded)
        {
            rb.linearVelocity = Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, JumpForce, rb.linearVelocity.z);
            IsGrounded = false;
            TryJump = false;
        }
        else if (IsGrounded) // Only allow movement control when grounded
        {
            if (moveDirection != Vector2.zero)
            {
                rb.linearVelocity = Vector3.Lerp(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                rb.linearVelocity = Vector3.Lerp(currentVelocity, new Vector3(0, currentVelocity.y, 0), deceleration * Time.fixedDeltaTime);
            }
        }
        else if (TryDash && !IsGrounded)
        {
            Vector3 dashDirection = move != Vector3.zero ? move : new Vector3(currentVelocity.x, 0, currentVelocity.z).normalized;
            rb.linearVelocity = new Vector3(dashDirection.x * DashForce, currentVelocity.y, dashDirection.z * DashForce);
            TryDash = false;
        }
        // If not grounded and not jumping, do nothing — velocity carries over freely
    }
}