using UnityEngine;

public class DestroyObject : MonoBehaviour
{   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ResetLevel();
        }

    }
}
