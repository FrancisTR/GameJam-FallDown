using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public BossCollision myBossCollision;
    public MinecartCollisionBoss myMinecartBoss;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    //[SerializeField] Vector3 targetPosition;
    [SerializeField] private Transform[] targetPosition;
    [SerializeField] int Lives = 3;
    SpriteRenderer m_SpriteRenderer;

    [SerializeField] public bool bossIsHit = false;

    [SerializeField] public bool isMinecart;

    [SerializeField] public bool isSkeleton;





    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();

        myRigidbody = GetComponent<Rigidbody2D>();

        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        myMinecartBoss = FindObjectOfType<MinecartCollisionBoss>();


        //BossCollision myBossCollision = bossCollision.GetComponent<BossCollision>();

    }
    private void FixedUpdate()
    {

        GameObject[] Minecarts = GameObject.FindGameObjectsWithTag("MinecartBoss");
        Debug.Log("num mines "+ Minecarts.Length.ToString());
        GameObject[] Skeletons = GameObject.FindGameObjectsWithTag("Skeleton");
        Debug.Log("num skeletons " +  Skeletons.Length.ToString());

        if (Skeletons.Length == 0 && Minecarts.Length == 0)
        {
            MissedHitOnBoss();
        }



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
 
        if(bossIsHit == false && isMinecart == false)
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
                Lives--;
                StartCoroutine(Damaged());
                TeleportBoss();
                Debug.Log(Lives);
                myBossCollision.Spawn();
                StartCoroutine(myBossCollision.SkeletonTimer());
                BossHitCheck();
            }


    }
    public void MinecartExist()
    {
        myAnimator.SetBool("isSummoning", false);
        GameObject[] Minecarts = GameObject.FindGameObjectsWithTag("MinecartBoss");
        if(Minecarts.Length == 0)
        {
            isMinecart = false;
            MissedHitOnBoss();
        }
        else if(Minecarts.Length > 0){
            isMinecart = true;
        }
    }
    public void SkeletonBossExist()
    {
        GameObject[] Skeletons = GameObject.FindGameObjectsWithTag("Skeleton");
        if (Skeletons.Length == 0)
        {
            isSkeleton = false;
        }
        else if(Skeletons.Length > 0)
        {
            isSkeleton = true;
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

    public void StopSummon()
    {
        myAnimator.SetBool("isSummoning", false);
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
        myAnimator.SetBool("isHurt", true);
        yield return new WaitForSeconds(1f);
        m_SpriteRenderer.color = Color.white;
        myAnimator.SetBool("isHurt", false);

    }
}