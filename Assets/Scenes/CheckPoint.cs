using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] public GameObject CheckMark;
    [SerializeField] private GameObject MessageIcon;
    private GameObject player;
    private GameObject savedPlayer;
    private bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown("w")) { 
            // EnterDoor();
            if(player){
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                if(playerMovement.setCheckPoint == this.gameObject){
                    playerMovement.setCheckPoint = null;
                    Debug.Log("toggled off");
                    CheckMark.gameObject.SetActive(false);
                }else{
                    if(playerMovement.setCheckPoint){
                        CheckPoint oldCheckPoint = playerMovement.setCheckPoint.GetComponent<CheckPoint>();
                        oldCheckPoint.CheckMark.gameObject.SetActive(false);
                    }
                    CheckMark.gameObject.SetActive(true);
                    playerMovement.setCheckPoint = this.gameObject;
                    Debug.Log("Set to "+this.gameObject.transform.position);
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("In Range of "+this+" CheckPoint");
            player = collider.gameObject;
            savedPlayer = collider.gameObject;
            playerInRange = true;
            MessageIcon.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
            MessageIcon.gameObject.SetActive(false);
        }
    }
}
