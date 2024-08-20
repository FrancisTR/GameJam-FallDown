using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooDrop : MonoBehaviour
{
    [SerializeField] GameObject SlimeSpawner;
    [SerializeField] float spawnLocation = 0.5f;
    [SerializeField] private float chanceToSpawn = 0.25f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        
        if(Random.value <= chanceToSpawn)
        {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + spawnLocation, transform.position.z);
        Instantiate(SlimeSpawner, spawnPosition, transform.rotation);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
