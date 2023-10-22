using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Transform collectibleTransform;
    [SerializeField] private float collectibleSpeed;
    private float maxHeight = 1.5f;
    private float lifeTime = 0.05f;
    private bool isCollision;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    private void Awake()
    {
        collectibleTransform = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        Collecting();
    }
    private bool CollisionController()
    {
        isCollision = Physics2D.OverlapBox(boxCollider.bounds.center, boxCollider.bounds.size, 0f, playerLayer);
        return isCollision;
    }
    private void Collecting()
    {
        CollisionController();
        if (isCollision)
        {
            collectibleTransform.position = new Vector3(transform.position.x,
                transform.position.y + maxHeight * collectibleSpeed,
                transform.position.z);

            GameManager.Instance.AddCoins();
            Destroy(gameObject, lifeTime);
        }
    }
}
