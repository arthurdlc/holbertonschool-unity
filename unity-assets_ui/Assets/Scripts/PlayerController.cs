using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float maxJumpHeight = 2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform characterModel; // 🔄 Modèle 3D (enfant de la capsule)

    private Rigidbody rb;
    private bool isGrounded;
    private float jumpVelocity;

    void Start()
    {
        Physics.gravity = new Vector3(0, -70f, 0);
        rb = GetComponent<Rigidbody>();
        jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * maxJumpHeight);
    }

    void Update()
    {
        HandleJump();
        HandleMovement();
        falling();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(horizontal, 0, vertical);
    
        if (moveInput.magnitude > 0)
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 moveDirection = cameraTransform.right * horizontal + cameraTransform.forward * vertical;
            moveDirection.y = 0;
            moveDirection.Normalize();

            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);

            Debug.Log("Player Rotation: " + transform.rotation.eulerAngles);
        }
    }


    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }

    private void falling()
    {
        if (rb.position.y < -10)
        {
            rb.position = new Vector3(0, 20, 0);
        }
    }
}
