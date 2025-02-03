using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    [SerializeField] private float _rotationSpeed = 3f;
    private Vector3 _offset;
    private float _yaw;

    private void Start()
    {
        _offset = Player.transform.position - this.transform.position;
        _yaw = transform.eulerAngles.y;
    }

    private void LateUpdate()
    {
        HandleCameraRotation();
        FollowPlayer();
    }

    private void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _rotationSpeed;
        _yaw += mouseX;
    }

    private void FollowPlayer()
    {
        transform.position = Player.transform.position - Quaternion.Euler(0f, _yaw, 0f) * _offset;
        transform.LookAt(Player.transform.position);
    }
}