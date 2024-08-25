using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartCollision : MonoBehaviour
{

    public MinecartController minecartController;
    // Start is called before the first frame update
    void Start()
    {
        minecartController = GetComponentInParent<MinecartController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)  
    {
        if(other.CompareTag("Player"))
        {
            minecartController.StartMoving();
            
        }
    }
    
}
