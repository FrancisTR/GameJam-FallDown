using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour

{
    [SerializeField] private float fallDelay = .15f;
    [SerializeField] private float destroyDelay = 1f;

    [SerializeField] private Rigidbody2D rb;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("touched");
            StartCoroutine(Fall());
        }
    }
    // Start is called before the first frame update
    private IEnumerator Fall()
    {
        Debug.Log("Falling");
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}
