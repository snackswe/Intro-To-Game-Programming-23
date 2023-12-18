using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogicHandler : MonoBehaviour
{
    // Global Variables:
    public static bool deathScreenUp = false;
    public static bool isPaused = false;
    bool endScreenUp = false;
    public static float time = 0;
    public GameObject deathScreen;
    public GameObject pauseScreen;
    public GameObject endScreen;
    public GameObject inPlayUI;
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        Debug.Log("start");
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        endScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused && !deathScreenUp)
            {
                ResumeGame();
            }
            else if (!deathScreenUp)
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
            score.text = "" + PlayerController.score;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                NextScene();
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                MainMenu();
            }
        }
        if (Time.timeScale == 0)
        {
            inPlayUI.SetActive(false);
        } else
        {
            inPlayUI.SetActive(true);
        }
    }
    public void Reload()
    {
            isPaused = false;
            string currentSceneName = SceneManager.GetActiveScene().name;
            deathScreenUp = false;
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(currentSceneName);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("CT main menu"); 
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void EndScreen()
    {
        Debug.Log("end screen");
        Time.timeScale = 0f;
        endScreen.SetActive(true);
        endScreenUp = true;
    }
    public void PauseGame()
    {
        Debug.Log("pause game");
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }    
    public void ResumeGame()
    {
        Debug.Log("resume game");
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
    public void YouDied()
    {
        Debug.Log("youDied");
        isPaused = true;
        Time.timeScale = 0f;
        deathScreenUp = true;
        deathScreen.SetActive(true);
    }
    public void DeathScreen()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            Reload();
        }
    }
}
