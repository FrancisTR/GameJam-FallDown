using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartCollisionBoss : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float secondsToDestroy = 5f;
    Rigidbody2D myRigidbody;
    public BossController bossController;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        bossController = GetComponent<BossController>();
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
    public IEnumerator DestroyMinecart()
    {

       yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
        bossController = FindObjectOfType<BossController>();
        bossController.MissedHitOnBoss();

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}