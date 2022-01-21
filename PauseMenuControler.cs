using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControler : MonoBehaviour
{

    public GameObject pauseUI;
    public bool paused = false;


    // Update is called once per frame
    void Update()
    {
        if (!paused) 
        {
            TimescaleOnPause();
        }
            
    }


    public void Resume()
    {
        //unflag
        paused = false;
        //reset timescale
        Time.timeScale = 1;
        //hide ui
        pauseUI.SetActive(false);
        //hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void backToMenu()
    {
        Time.timeScale = 1;

        Destroy(GameObject.Find("MainMenuCanvas"));
        Destroy(GameObject.Find("HighScoreControler"));
        SceneManager.LoadScene(0);
    }


    public void TimescaleOnPause()
    {
        //get pause button down
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //flag paused
            paused = true;

            //timescale down for pause effect
            Time.timeScale = 0.01f;

            //show pause UI
            pauseUI.SetActive(true);

            Cursor.lockState = CursorLockMode.Confined;
        }
    }


}
