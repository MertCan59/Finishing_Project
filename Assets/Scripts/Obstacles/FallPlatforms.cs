using UnityEngine;

public class FallPlatforms : MonoBehaviour
{
    private bool isTouching;
    [SerializeField] private float downSpeed;
    [SerializeField] private Rigidbody2D rigidbody;
    private Transform fallTransform;
    private float lifeTime = 5f;
    private bool isTriggerEntered;
    private void Awake()
    {
        isTouching = false;   
        isTriggerEntered = false;
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
        if (collision.gameObject.CompareTag("Eraser"))
        {
            isTriggerEntered = true;
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
        }
    }
    private void Invoker()
    {
        Invoke(nameof(Move),lifeTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
