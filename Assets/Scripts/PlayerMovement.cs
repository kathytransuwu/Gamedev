
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

    bool IsGrounded;
    bool TryJump;

    Vector2 moveDirection = Vector2.zero;

    private void OnEnable()
    {
        Controls.Enable();
        JumpAction.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
        JumpAction.Disable();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Reads movement and assigns value to them
        moveDirection = Controls.ReadValue<Vector2>();

        //If the player presses space AND is grounded, 
        if(JumpAction.triggered && IsGrounded)
        {
            TryJump = true;
        }
    }

    private void FixedUpdate()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        Vector3 move = transform.right * moveDirection.x + transform.forward * moveDirection.y;
        move.Normalize();

        Vector3 currentvelocity = rb.linearVelocity;
        
        


        if (TryJump)
        {
            rb.linearVelocity = new Vector3(currentvelocity.x, JumpForce, currentvelocity.z);
            IsGrounded = false;
            TryJump = false;
        }

        if (IsGrounded)
        {
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
}