using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollision : MonoBehaviour
{
    public BossController bossController;
    [SerializeField] public GameObject SkeletonSpawner;
    [SerializeField] public GameObject MinecartBossSpawner;
    [SerializeField] public Transform[] SkeletonSpawnPoint;
    [SerializeField] public Transform[] MinecartBossSpawnPoint;
    Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        bossController = GetComponentInParent<BossController>();
        myCollider = gameObject.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (myCollider != null && other.CompareTag("Player") || other.CompareTag("Enemy")  && myCollider.enabled)
        {
            bossController.StartSummon();
            Spawn();

            StartCoroutine(SkeletonTimer());
            myCollider.enabled = false;
        }
    }

    public IEnumerator SkeletonTimer()
    {
        Debug.Log("destroying");
        yield return new WaitForSeconds(5f);
        Debug.Log("finished");
        DestroyAllSkeletons();
    }

    public void Spawn()
    {
        foreach (Transform spawnPoint in SkeletonSpawnPoint)
        {
            Instantiate(SkeletonSpawner, spawnPoint.position, spawnPoint.rotation);

        }

    }
    public void SpawnMinecartBoss()
    {
        
        foreach (Transform spawnPoint in MinecartBossSpawnPoint)
        {
            Instantiate(MinecartBossSpawner, spawnPoint.position, spawnPoint.rotation);

        }

    }

    void DestroyAllSkeletons()
{
        Debug.Log("in destroying");
        GameObject[] Skeletons = GameObject.FindGameObjectsWithTag("Skeleton");
        SpawnMinecartBoss();
        foreach (GameObject skeleton in Skeletons)
        {
            Debug.Log("goodbye skellies");
            if (skeleton.scene.IsValid())
            {
                Destroy(skeleton);
            }
        }
    }
}
