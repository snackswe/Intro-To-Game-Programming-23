using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogicHandler : MonoBehaviour
{
    // Global Variables:
    public static bool deathScreenUp;
    public static bool isPaused;
    bool endScreenUp;
    public static int score;
    public GameObject deathScreen;
    public GameObject pauseScreen;
    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        endScreen.SetActive(false);
        isPaused = false;
        deathScreenUp = false;
        endScreenUp = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused && !deathScreenUp)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
            
        }
        if (deathScreenUp)
        {
            DeathScreen();
        }
        else if (endScreenUp)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene("CT main menu");
            }
        }
    }
    public void EndScreen()
    {
        Time.timeScale = 0f;
        endScreen.SetActive(true);
        endScreenUp = false;
    }
    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }    
    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
    public void YouDied()
    {
        isPaused = true;
        Time.timeScale = 0f;
        deathScreenUp = true;
        deathScreen.SetActive(true);
    }
    public void DeathScreen()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            isPaused = true;
            string currentSceneName = SceneManager.GetActiveScene().name;
            deathScreenUp = false;
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
