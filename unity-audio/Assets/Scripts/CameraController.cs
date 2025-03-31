using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public bool isInverted = false;

    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _heightOffset = 2f; // Hauteur ajustée
    [SerializeField] private float _distance = 5f; // Distance derrière le joueur

    private float _yaw;
    private float _pitch;

    private void Start()
    {
        Vector3 startRotation = transform.eulerAngles;
        _yaw = startRotation.y;
        _pitch = startRotation.x;

        if (SharedInfo.Instance.InvertY)
        {
            isInverted = true;
        }
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
        _pitch = Mathf.Clamp(_pitch, -30f, 60f); // Limite l'angle vertical

        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0f);
        Vector3 offset = rotation * new Vector3(0, _heightOffset, -_distance);

        transform.position = Player.transform.position + offset;
        transform.LookAt(Player.transform.position + Vector3.up * _heightOffset);
    }

    private void FollowPlayer()
    {
        // La caméra suit le joueur avec la rotation appliquée
        Vector3 targetPosition = Player.transform.position + Quaternion.Euler(_pitch, _yaw, 0f) * new Vector3(0, _heightOffset, -_distance);
        transform.position = targetPosition;
    }
}
