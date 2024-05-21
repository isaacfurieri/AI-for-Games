using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void MainMenu()
    {
        
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}