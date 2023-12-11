using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    int currentScene;
    public SceneLogicHandler sceneMan;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !SceneLogicHandler.isPaused && !SceneLogicHandler.deathScreenUp)
        {
            sceneMan.EndScreen();
        }
    }
}
