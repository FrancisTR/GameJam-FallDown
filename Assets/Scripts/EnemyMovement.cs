using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f; 
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();   
    }
    void FlipEnemyFacing()
    {
         transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }
}
