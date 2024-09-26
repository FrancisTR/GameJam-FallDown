using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI; 
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public TextMeshProUGUI livesLabel;
    private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 18f;
    private bool isFacingRight = true;
    [SerializeField] int Lives = 3;
    Animator myAnimator;
    SpriteRenderer m_SpriteRenderer;
    BoxCollider2D  myBoxCollider;
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject player;
    [SerializeField] bool inGoo;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float attackRange = 1f; // The range of the melee attack
    [SerializeField] private int attackDamage = 10; // The damage dealt by the attack
    [SerializeField] private Transform attackPoint; // The point from where the attack originates
    [SerializeField] private LayerMask enemyLayers; // The layers considered as enemies
    // Start is called before the first frame update
    void Start()
     {
        myAnimator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        if (!livesLabel)
        {
            GameObject labelObject = GameObject.Find("StaticCanvas/LivesTextTMP");
            if (labelObject)
            {
                livesLabel = labelObject.GetComponent<TextMeshProUGUI>();
                livesLabel.text = "Lives: " + Lives.ToString();
            }
            else
            {
                Debug.LogError("LivesLabel GameObject not found!");
            }
        }
        // Tilemap tilemap = GetComponent<Tilemap>();
     }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded() && inGoo == false){
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
        if(Input.GetKeyDown("s"))
            {
                Debug.Log("Destroying goo");
                DestroyGooTile(groundCheck.transform.position);
            }
        if (Input.GetKeyDown(KeyCode.E))
        {

            myAnimator.SetTrigger("isHitting");
            PerformMeleeAttack();
        }

        Flip();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
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
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Goo"))
        {
            inGoo = true;
            speed = 2f;
        }
        else
        {
            speed = 8f;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        inGoo = false;
        speed = 8f;
    }
     private void DestroyGooTile(Vector3 worldPosition)
        {
            Vector3Int gooTilePosition = tilemap.WorldToCell(worldPosition);
            
            tilemap.SetTile(gooTilePosition, null);

            Debug.Log("Tile destroyed at: " + gooTilePosition);
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
            livesLabel.text = "You've died!";
            StartCoroutine(DeathAnimation());
        }
        else
        {
            livesLabel.text = "Lives: " + Lives.ToString();
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

// Method to perform the melee attack
    private void PerformMeleeAttack()
    {
        // Detect all enemies in the attack range using Physics2D.OverlapCircleAll
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Loop through all hit enemies and apply damage
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            //enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    // Visualize the attack range in the Unity editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
