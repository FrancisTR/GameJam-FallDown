using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour

{
    [SerializeField] private float fallDelay = .15f;
    [SerializeField] private float destroyDelay = 1f;

    [SerializeField] private Rigidbody2D rb;
    BoxCollider2D myBoxCollider;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        // Debug.Log(myBoxCollider);
    }
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
        myBoxCollider.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}
