using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
// using UnityEditor.Tilemaps;
using UnityEditor.XR;
using UnityEngine;

public class ArrowRight : MonoBehaviour
{
    [SerializeField] private float ArrowSpeed = 20f;
    [SerializeField] private float timeToDestroy = 2f;

    Rigidbody2D myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.velocity = new Vector2(ArrowSpeed, 0f);
        StartCoroutine(DestroyArrow());
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
