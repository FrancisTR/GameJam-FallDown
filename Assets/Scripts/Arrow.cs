using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditor.XR;
using UnityEngine;

public class Arrow : MonoBehaviour
{
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

    

}
