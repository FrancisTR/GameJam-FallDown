using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 18f;
    private bool isFacingRight = true;
    [SerializeField] int Lives = 3;
    Animator myAnimator;
    SpriteRenderer m_SpriteRenderer;
    BoxCollider2D  myBoxCollider;




    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
     {
        myAnimator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        myBoxCollider = GetComponent<BoxCollider2D>();
     }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded()){
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if(rb.velocity.y != 0 && !IsGrounded())
        {
            myAnimator.SetBool("isJumping",true);
        }
        else
        {
            myAnimator.SetBool("isJumping",false);
        }


        Flip();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Goo"))
        {
            speed = 2f;
        }
        else
        {
            speed = 8f;
        }
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("MinecartBoss"))
        {
            StartCoroutine(Damaged());
            Destroy(collision.gameObject);
            HealthCheck();
        }
        if(collision.gameObject.CompareTag("Hazards"))
        {
            Lives = 0;
            HealthCheck();
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        bool playerHasMovementSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isWalking", playerHasMovementSpeed);
    }

    private bool IsGrounded(){
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip(){
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f ){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void HealthCheck() 
    {
        Debug.Log("checking");
            if(Lives <= 0)
            {
                myAnimator.SetBool("isDying", true);
                StartCoroutine(DeathAnimation());
                
            }
    }
    IEnumerator  DeathAnimation()
    {
        Debug.Log("dead");
        rb.bodyType = RigidbodyType2D.Static;
        myBoxCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Damaged()
    {
        m_SpriteRenderer.color = Color.red;
        Lives--;
        yield return new WaitForSeconds(.1f);
        m_SpriteRenderer.color =  Color.white;
    }

}
