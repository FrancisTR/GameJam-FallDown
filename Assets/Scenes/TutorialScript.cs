using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TutorialScript : MonoBehaviour
{


    [SerializeField] private MessageUI CutsceneMessage;
    [SerializeField] private MessageUI triggerMessage;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Collider2D targetArea;
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private CinemachineConfiner targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        CutsceneMessage.gameObject.SetActive(true);
        player.restrictMovement = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerMessage.triggeredFinish){
            player.restrictMovement = false;
            targetCamera.m_BoundingShape2D = targetArea;
            camera.Follow = player.transform;
            camera.m_Lens.OrthographicSize = 5;
            triggerMessage.triggeredFinish = false;
        }
    }
}
