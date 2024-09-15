using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

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
    private BoxCollider2D myBoxCollider;
    private Controls input = null;
    private InputAction buttonAction;
    private GameObject player;
    private bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (playerInRange && buttonAction.triggered)
        {
            enterDoor();
        }
    }
    private void Awake()
    {
        // Initialize the controls
        input = new Controls();
        Debug.Log(input.Action.Interaction);
        buttonAction = input.Action.Interaction;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("In Range of "+this+" Door");
            player = collider.gameObject;
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }
     private void OnEnable()
    {
        // Enable the input action
        buttonAction.Enable();
    }
    private void OnDisable()
    {
        // Disable the input action
        buttonAction.Disable();
    }

    private void enterDoor()
    {
        if(!player || !targetDoor)
        {
            return;
        }
        player.transform.position = targetDoor.transform.position;
        targetCamera.m_BoundingShape2D = targetArea;
    }
}
