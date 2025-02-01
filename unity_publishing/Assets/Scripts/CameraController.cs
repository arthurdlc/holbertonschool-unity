using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    private void Start()
    {
        offset = player.transform.position - this.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        this.transform.position = player.transform.position - offset;
    }
}