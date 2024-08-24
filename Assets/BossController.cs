using System.Collections;
using System.Collections.Generic;
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


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();

        myRigidbody = GetComponent<Rigidbody2D>();

        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        //BossCollision myBossCollision = bossCollision.GetComponent<BossCollision>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Minecart"))
        {
            transform.position = targetPosition[Lives - 1].position;
            StartCoroutine(Damaged());
            Debug.Log(Lives);
            myBossCollision.Spawn();
            StartCoroutine(myBossCollision.SkeletonTimer());
        }

    }


    // Update is called once per frame
    void Update()
    {
        
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
