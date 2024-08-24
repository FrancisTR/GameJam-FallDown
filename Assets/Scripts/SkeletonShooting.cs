using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShooting : MonoBehaviour
{
    [SerializeField] private GameObject Arrow;
    [SerializeField] private Transform ArrowSpawn;
    [SerializeField] private float shootingInterval = 1f;
    void Start()
    {
        StartCoroutine(ShootingArrow());
    }
    void Update()
    {
       
    }

    private IEnumerator ShootingArrow()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootingInterval);
            Instantiate(Arrow, ArrowSpawn.position, transform.rotation);
        }
    }
}
