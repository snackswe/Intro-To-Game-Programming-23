using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Level1()
    {
        SceneManager.LoadScene("CT Level 1");
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("CT Tutorial");
    }
    public void Menu()
    {
        SceneManager.LoadScene("CT main menu");
    }
}
