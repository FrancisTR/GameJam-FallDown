using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditor.XR;
using UnityEngine;

public class Arrow : MonoBehaviour
{
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
}
