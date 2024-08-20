using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooSpawner : MonoBehaviour
{

    [SerializeField] private GameObject gooDrop;
    [SerializeField] private float gooDropInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GooSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator GooSpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(gooDropInterval);
            Instantiate(gooDrop, transform.position, transform.rotation);
        }

    }
    
}
