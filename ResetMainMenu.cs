using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetMainMenu : MonoBehaviour
{

    bool leaveMenuFlag = false;
    public GameObject RemoteHSManager;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {

        if (!(scene.name == "Main Menu") && leaveMenuFlag == false && !(scene.name == "Multiplayer"))
        {
            //if leave menu
            leaveMenuFlag = true;
            RemoteHSManager.GetComponent<RemoteHighScoreManager>().charhide();
            Debug.Log("char hide");
        }
    }
}
