using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
   
    [SerializeField] private GameObject arrowPrefab; // The arrow prefab to instantiate
    [SerializeField] private Transform arrowSpawnPoint; // The point from where the arrow is shot
    [SerializeField] private float shootInterval = 2f;

    private Transform player; // Reference to the player
    private float nextShootTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
        nextShootTime = Time.time;
    }

    void Update()
    {
        // Make the skeleton face the player
        FacePlayer();

        // Handle shooting arrows at regular intervals
        if (Time.time >= nextShootTime)
        {
            ShootArrow();
            nextShootTime = Time.time + shootInterval; // Schedule the next shoot time
        }
    }

    private void FacePlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = player.position - transform.position;

        // Determine if the skeleton should face left or right
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Flip the skeleton's localScale to make it face the other direction
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void ShootArrow()
    {
        // Instantiate the arrow
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        // Pass the direction of the skeleton to the arrow
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.SetDirection(transform.localScale.x > 0 ? 1 : -1);
    }
}


