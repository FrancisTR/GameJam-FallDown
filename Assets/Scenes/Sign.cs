using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{

    private GameObject player;
    private bool playerInRange = false;

    [SerializeField] private GameObject MessageIcon;
    [SerializeField] private MessageUI MessageUI;

    // Start is called before the first frame update
    void Start()
    {
        // Tilemap tilemap = GetComponent<Tilemap>();
        // Debug.Log("test   "+ MessageUI.gameObject.activeInHierarchy);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown("w")) { 
            MessageUI.gameObject.SetActive(true);
            MessageIcon.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("In Range of "+this+" Sign");
            player = collider.gameObject;
            playerInRange = true;
            MessageIcon.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
            MessageIcon.SetActive(false);
            MessageUI.gameObject.SetActive(false);
        }
    }
}
