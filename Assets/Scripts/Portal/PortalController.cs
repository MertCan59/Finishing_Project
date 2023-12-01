using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private GameObject player;
    [SerializeField] private float time = 1.5f;
    private bool isTouching;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        StartCoroutine(Teleport());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouching = true;
        }
    }
    private IEnumerator Teleport()
    {
        if (isTouching)
        {
            if(Vector2.Distance(player.transform.position, transform.position) > 1.5f)
            {
                yield return new WaitForSeconds(time);
                player.transform.position = destination.transform.position;
            }
            yield return new WaitForSeconds(time);
            isTouching = false;
        }
        
    }
}
