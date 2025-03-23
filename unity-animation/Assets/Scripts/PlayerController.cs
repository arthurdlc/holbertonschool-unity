using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float maxJumpHeight = 2f;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    private bool isGrounded;
    private float jumpVelocity;
    private Transform characterMesh;
    private Animator animator;

    void Start()
    {
        Physics.gravity = new Vector3(0, -70f, 0);
        rb = GetComponent<Rigidbody>();
        jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * maxJumpHeight);
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        characterMesh = transform.GetChild(0);
        animator = characterMesh.GetComponent<Animator>();
    }

    void Update()
    {
        HandleJump();
        HandleMovement();
        CheckLanding();
        falling();

        if (characterMesh != null)
        {
            characterMesh.localPosition = new Vector3(0, -1.1f, 0);
            characterMesh.localRotation = Quaternion.identity;
        }
    }

    private void HandleMovement()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        float speed = rb.linearVelocity.magnitude;

        if (moveInput.magnitude > 0)
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 moveDirection = cameraTransform.right * moveInput.x + cameraTransform.forward * moveInput.z;
            moveDirection.y = 0;
            moveDirection.Normalize();

            transform.rotation = Quaternion.LookRotation(moveDirection);
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", speed);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpVelocity, rb.linearVelocity.z);
            animator.SetBool("IsJumping", true);
            Debug.Log("Jump Triggered"); // Vérifier que l'animation est bien activée
        }
    }

    private void CheckLanding()
    {
        if (isGrounded && animator.GetBool("IsJumping"))
        {
            animator.SetBool("IsJumping", false);
            Debug.Log("Landed - IsJumping set to false"); // Vérifier que le retour au sol est bien détecté
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
            CheckLanding();
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
