using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // on serialise les variables pour pouvoir les modifier dans l'inspecteur
    [SerializeField] private float maxJumpHeight = 2f; // idem
    [SerializeField] private LayerMask groundLayer; // idem
    private Rigidbody rb;
    private bool isGrounded;
    private float jumpVelocity;

    void Start() // initialisation de la gravité, et de la vitesse de saut ainsi que l'attibution du rigidbody
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
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); // Use GetAxisRaw

        if (moveInput.magnitude > 0)
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 moveDirection = cameraTransform.right * moveInput.x + cameraTransform.forward * moveInput.z;
            moveDirection.Normalize();

            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0); // Stop horizontal movement when no input
        }
    }

    private void HandleJump() // les sauts sont geré a part
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);
        }
    }

    private void OnCollisionStay(Collision collision) // ici le joueur reste sur le sol donc il peut sauter
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision) // pour check si le jouueur a quitté le sol pour l'empecher de faire des sauts en l'air 
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
    private void falling() // si le joueur tombe, il perd
    {
        if (rb.position.y < -10)
        {
            rb.position = new Vector3(0, 20, 0);
        }
    }
}