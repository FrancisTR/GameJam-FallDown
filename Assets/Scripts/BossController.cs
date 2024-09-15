using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public BossCollision myBossCollision;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    //[SerializeField] Vector3 targetPosition;
    [SerializeField] private Transform[] targetPosition;
    [SerializeField] int Lives = 3;
    SpriteRenderer m_SpriteRenderer;

    [SerializeField] public bool bossIsHit = false;

    [SerializeField] public bool isMinecart;
    


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();

        myRigidbody = GetComponent<Rigidbody2D>();

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        

        //BossCollision myBossCollision = bossCollision.GetComponent<BossCollision>();
        
    }
    private void Update() 
    {
        // MinecartExist();
    }
    public void BossHitCheck(){
        Debug.Log("BossHIt");
        bossIsHit = false;
    }
    public void MissedHitOnBoss()
    {
        Debug.Log("Hello");
        Debug.Log(bossIsHit);
        Debug.Log(isMinecart);
        GameObject[] Minecarts = GameObject.FindGameObjectsWithTag("MinecartBoss");
        if(Minecarts.Length == 0)
        {
            isMinecart = false;
        }
        else if (Minecarts.Length > 0){
            isMinecart = true;
        }
        if(!(bossIsHit && isMinecart))
        {
        Debug.Log("Hello NO HIT?");
            myBossCollision.Spawn();
            StartCoroutine(myBossCollision.SkeletonTimer());
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
    
        if(other.CompareTag("MinecartBoss"))
        {
            bossIsHit = true;
        }
        
        
            if(bossIsHit)
            {
                TeleportBoss();
                Lives--;
                StartCoroutine(Damaged());
                Debug.Log(Lives);
                myBossCollision.Spawn();
                StartCoroutine(myBossCollision.SkeletonTimer());
            }
            
        
    }
    public void MinecartExist()
    {
        GameObject[] Minecarts = GameObject.FindGameObjectsWithTag("MinecartBoss");
        if(Minecarts.Length == 0)
        {
            isMinecart = false;
        }
        else{
            isMinecart = true;
        }
    }
    private void TeleportBoss()
    {
         int target   = Random.Range(0, 3);
        while(transform.position == targetPosition[target].position)
        {
            target   = Random.Range(0, 3); 
            Debug.Log("Same Number");
        }
            transform.position = targetPosition[target].position;

    }


    
    public void StartSummon()
    {
        myAnimator.SetBool("isSummoning", true);
    }

    void HealthCheck()
    {
        Debug.Log("checking");
        if (Lives <= 0)
        {
            //myAnimator.SetBool("isDying", true);
            //StartCoroutine(DeathAnimation());

        }
    }

    IEnumerator Damaged()
    {
        m_SpriteRenderer.color = Color.red;
        Lives--;
        yield return new WaitForSeconds(.1f);
        m_SpriteRenderer.color = Color.white;
    }
}
