using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float maxJumpHeight = 2f;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    private bool isGrounded;
    private float jumpVelocity;
    private Animator animator;

    void Start()
    {
        Physics.gravity = new Vector3(0, -70f, 0);
        rb = GetComponent<Rigidbody>();
        jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * maxJumpHeight);
        animator = GetComponent<Animator>();  // Référence à l'Animator
    }

    void Update()
    {
        HandleJump();
        HandleMovement();
        falling();
    }

    private void HandleMovement()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (moveInput.magnitude > 0)
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 moveDirection = cameraTransform.right * moveInput.x + cameraTransform.forward * moveInput.z;
            moveDirection.y = 0; 
            moveDirection.Normalize();

            transform.rotation = Quaternion.LookRotation(moveDirection);  // Appliquer la rotation

            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);  // Appliquer le mouvement

            animator.SetFloat("Speed", moveInput.magnitude * moveSpeed);  // Mettre à jour le paramètre 'Speed' de l'Animator
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);  // Stopper le mouvement horizontal
            animator.SetFloat("Speed", 0);  // Réinitialiser 'Speed' dans l'Animator
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);  // Appliquer la vitesse de saut
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;  // Le joueur est au sol
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;  // Le joueur quitte le sol
        }
    }

    private void falling()
    {
        if (rb.position.y < -10)
        {
            rb.position = new Vector3(0, 20, 0);  // Réinitialiser la position en cas de chute
        }
    }
}
