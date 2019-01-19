using UnityEngine;

public class CameraPlayerController : MonoBehaviour
{
    private Vector3 offset;
    public GameObject player;

    // Use this for initialization
    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}