using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Check/Add in Inpector Properties
    // Displays Score on Game over Screen
    public TextMeshProUGUI scoreText;
    
    // This will make the Game Over screen Visible
    // And the score will be passed from the 
    // TopDownMovement component
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString();
    }

    // This will restart the game
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // This will navigate to main menu
    public void ExitButton()
    {

    }
}
