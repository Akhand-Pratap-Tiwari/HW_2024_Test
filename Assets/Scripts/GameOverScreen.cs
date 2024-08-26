using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Check/Add in Inpector Properties
    public TextMeshProUGUI scoreText;
    
    // Start is called before the first frame update
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitButton()
    {

    }
}
