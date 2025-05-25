using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image lifeImagesDisplay;
    public TextMeshProUGUI scoreText;
    public GameObject titleScreen;
    public int score;

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Updating Lives: " + currentLives);
        lifeImagesDisplay.sprite = lives[currentLives];


    }
    
    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score.ToString();
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);

    }
}
