using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    /*
    For this class to function you need:
        - A box collider
            - set it to is trigger
        - Have a camera
    */
    [SerializeField] private DoorTransition targetDoor;
    [SerializeField] private Collider2D targetArea;
    [SerializeField] private CinemachineConfiner targetCamera;
    [SerializeField] private GameObject MessageIcon;
    [SerializeField] private string nextScene;
    [SerializeField] private bool forceSpawn;
    private BoxCollider2D myBoxCollider;
    private GameObject player;
    private GameObject savedPlayer;
    private bool playerInRange = false;


    // Start is called before the first frame update
    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (playerInRange && Input.GetKeyDown("w")) { 
            EnterDoor();
        }
        if(playerInRange && nextScene != "" && Input.GetKeyDown("w")){
            SceneManager.LoadScene(nextScene);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(forceSpawn && savedPlayer && savedPlayer.GetComponent<PlayerMovement>().triggerForceSpawn){
            PlayerMovement playerMovement = savedPlayer.GetComponent<PlayerMovement>();
            playerMovement.triggerForceSpawn = false;
            savedPlayer.transform.position = this.transform.position;
        }
    }
    private void Awake()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("In Range of "+this+" Door");
            player = collider.gameObject;
            savedPlayer = collider.gameObject;
            playerInRange = true;
            showMessage(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
            showMessage(false);
        }
    }

    private void showMessage(bool b){
        if((MessageIcon && targetDoor)||(MessageIcon && nextScene != "")){
            MessageIcon.gameObject.SetActive(b);
        }
    }

    private void EnterDoor()
    {
        if(!player || !targetDoor)
        {
            return;
        }
        player.transform.position = targetDoor.transform.position;
        targetCamera.m_BoundingShape2D = targetArea;
    }
}
