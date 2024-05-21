using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public GameObject Loose;
    public GameObject Win;
    // Start is called before the first frame update
    public void GameOver(bool gameOver)
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        
        if(gameOver)
        {
            Loose.SetActive(false);
        }
        else
        {
            Win.SetActive(false);
        }
    }
    public void RestartButton() 
    {
        SceneManager.LoadScene("GameScene");
    }
    public void MainMenuButton() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
