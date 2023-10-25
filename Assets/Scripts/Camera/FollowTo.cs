using UnityEngine;

public class FollowTo : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
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
        cameraPosition = new Vector3(player.position.x + xOffset, player.position.y+ yOffset, -10f);
        transform.position = cameraPosition;

    }
}
