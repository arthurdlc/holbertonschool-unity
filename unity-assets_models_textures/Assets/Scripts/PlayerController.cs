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
        HandleMovement();
        HandleJump();
        falling();
    }

    private void HandleMovement() // ici on adapte la gravitée pour un trucs plus realiste ( chiant de faire un suat de 2 pendant 5 secondes)
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        rb.linearVelocity = new Vector3(moveInput.x * moveSpeed, rb.linearVelocity.y, moveInput.z * moveSpeed);
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