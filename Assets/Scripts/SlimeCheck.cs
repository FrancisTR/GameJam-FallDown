using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeCheck
    : MonoBehaviour
{
    public PlayerMovement myPlayer;
    [SerializeField] private Image slimeImage; // UI Image to display lives
    [SerializeField] private Sprite[] slimeSprites; // Array of sprites for different lives (0-5)


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar();

    }

    public void HealthBar()
    {

        switch (myPlayer.Slimes)
        {
            case 10:
                slimeImage.sprite = slimeSprites[10];
                break;
            case 9:
                slimeImage.sprite = slimeSprites[9];
                break;
            case 8:
                slimeImage.sprite = slimeSprites[8];
                break;
            case 7:
                slimeImage.sprite = slimeSprites[7];
                break;
            case 6:
                slimeImage.sprite = slimeSprites[6];
                break;
            case 5:
                slimeImage.sprite = slimeSprites[5];
                break;
            case 4:
                slimeImage.sprite = slimeSprites[4];
                break;
            case 3:
                slimeImage.sprite = slimeSprites[3];
                break;
            case 2:
                slimeImage.sprite = slimeSprites[2];
                break;
            case 1:
                slimeImage.sprite = slimeSprites[1];
                break;
            case 0:
                slimeImage.sprite = slimeSprites[0];
                break;
                // code block
        }
    }
}
