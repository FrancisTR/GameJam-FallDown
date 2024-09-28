using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(FindObjectOfType<PlayerMovement>().transform.position);
    }

    public void test(){
        Debug.Log("ahahdsfahdsfsdaaaaaaaaaaa");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
