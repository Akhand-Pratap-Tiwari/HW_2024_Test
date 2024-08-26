using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MidGamePause : MonoBehaviour
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
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // To resume the game
    public void ResumeButton()
    {
        gameObject.SetActive(false);
    }

    // To quit the game
    public void QuitGameButton()
    {
        Application.Quit();
    }
}
