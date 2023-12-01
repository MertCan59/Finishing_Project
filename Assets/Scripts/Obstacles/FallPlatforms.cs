using UnityEngine;

public class FallPlatforms : MonoBehaviour
{
    private bool isTouching;
    [SerializeField] private float downSpeed;
    private Transform fallTransform;
    private float lifeTime = 1f;
    private void Awake()
    {
        isTouching = false;   
        fallTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        Invoker();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouching = false;
        }
    }
    private void Move()
    {
        if (isTouching)
        {
            fallTransform.position = new Vector3(
                fallTransform.position.x,
                fallTransform.position.y - downSpeed*Time.deltaTime,
                fallTransform.position.z
                );
            Destroy(gameObject,lifeTime);
        }
    }
    private void Invoker()
    {
        Invoke(nameof(Move),lifeTime);
    }
}
