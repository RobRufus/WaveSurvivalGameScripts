using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControlerM : MonoBehaviour
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
        //hide ui
        pauseUI.SetActive(false);
        //hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void backToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


    public void TimescaleOnPause()
    {
        //get pause button down
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //flag paused
            paused = true;

            //show pause UI
            pauseUI.SetActive(true);

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }


}
