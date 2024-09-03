using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartCollisionBoss : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float secondsToDestroy = 5f;
    Rigidbody2D myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        StartMoving();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMoving()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        StartCoroutine(DestroyMinecart());
    }
    IEnumerator DestroyMinecart()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }
}