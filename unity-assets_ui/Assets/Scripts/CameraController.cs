using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public bool isInverted; // Correction : Toggle remplacé par un bool
    [SerializeField] private float _rotationSpeed = 3f;
    private Vector3 _offset;
    private float _yaw;
    private float _pitch;

    private void Start()
    {
        _offset = Player.transform.position - transform.position;
        _yaw = transform.eulerAngles.y;
        _pitch = transform.eulerAngles.x;
    }

    private void LateUpdate()
    {
        HandleCameraRotation();
        FollowPlayer();
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * _rotationSpeed * (isInverted ? 1 : -1); // Correction de l'inversion

        _yaw += mouseX;
        _pitch += mouseY; // Correction : plus logique pour inverser correctement

        // Clamp l'angle pour éviter un retournement complet
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
    }

    private void FollowPlayer()
    {
        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);
        transform.position = Player.transform.position - rotation * _offset;
        transform.LookAt(Player.transform.position);
    }
}
