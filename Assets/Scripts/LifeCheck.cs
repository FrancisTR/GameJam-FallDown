using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCheck : MonoBehaviour
{
    public PlayerMovement myPlayer;
    [SerializeField] private Image healthImage; // UI Image to display lives
    [SerializeField] private Sprite[] lifeSprites; // Array of sprites for different lives (0-5)


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

        switch (myPlayer.Lives)
        {
            case 5:
                healthImage.sprite = lifeSprites[5];
                break;
            case 4:
                healthImage.sprite = lifeSprites[4]; break;
            case 3:
                healthImage.sprite = lifeSprites[3]; break;
            case 2:
                healthImage.sprite = lifeSprites[2]; break;
            case 1:
                healthImage.sprite = lifeSprites[1]; break;
            case 0:
                healthImage.sprite = lifeSprites[0]; break;
                // code block
        }
    }
}
