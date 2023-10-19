using UnityEngine;

public class FollowTo : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float offset;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        CameraFollow();
    }
    private void CameraFollow()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition = new Vector3(player.position.x + offset, player.position.y, -10f);
        transform.position = cameraPosition;

    }
}
