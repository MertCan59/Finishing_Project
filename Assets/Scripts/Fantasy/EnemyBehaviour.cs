using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject playerObject;
    [SerializeField] private float speed;
    void Start()
    {
        playerObject=GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        Vector3 playerPosition = playerObject.transform.position;
        Vector3 direction=(playerPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;   
    }
}
