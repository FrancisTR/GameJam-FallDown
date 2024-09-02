using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditor.XR;
using UnityEngine;

public class Arrow : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] private float arrowSpeed = 20f;
    [SerializeField] private float timeToDestroy = 2f;

    private Rigidbody2D myRigidbody;
    private int direction; // 1 for right, -1 for left

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        // Set the velocity based on the direction (left or right)
        myRigidbody.velocity = new Vector2(direction * arrowSpeed, 0f);

        // Flip the arrow if it's moving left
        if (direction < 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

        // Start the coroutine to destroy the arrow after a certain time
        StartCoroutine(DestroyArrow());
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
=======
    [SerializeField] private float ArrowSpeed = 20f;
    Rigidbody2D myRigidbody;
    GameObject skeletonLeft;
    GameObject skeletonRight;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        skeletonLeft = GameObject.FindWithTag("SkeletonLeft");
        skeletonRight = GameObject.FindWithTag("SkeletonRight");
        if(skeletonLeft)
        {
            ShootLeft();
            return;
        }
        else if(skeletonRight)
        {
            ShootRight();
            return;
        }
        else
        {
            return;
        }
    }

    private void ShootLeft()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        myRigidbody.velocity = new Vector2(-ArrowSpeed, 0f);

    } 
    private void ShootRight()
    {
        myRigidbody.velocity = new Vector2(ArrowSpeed, 0f);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);   
    }

    

>>>>>>> 4d82d57c79266825a29ace6de564538355aa052f
}
