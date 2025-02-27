using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public bool isInverted = false;
    [SerializeField] private float _rotationSpeed = 3f;
    private Vector3 _offset;
    private float _yaw;
    private float _pitch;

    private void Start()
    {
        _offset = Player.transform.position - this.transform.position;
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
        float mouseY = Input.GetAxis("Mouse Y") * _rotationSpeed;

        _yaw += mouseX;
        _pitch += isInverted ? mouseY : -mouseY;

        // Clamp the pitch to prevent flipping
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
    }

    private void FollowPlayer()
    {
        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);
        transform.position = Player.transform.position - rotation * _offset;
        transform.LookAt(Player.transform.position);
    }
}