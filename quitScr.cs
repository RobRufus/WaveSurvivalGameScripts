using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class quitScr : MonoBehaviour
{

    public static quitScr QS;

    public GameObject MainMenuCanvas;
    public GameObject GameSelectCanvas;
    public GameObject CharacterSelectCanvas;
    public GameObject MapSelectCanvas;
    public GameObject LobbyConnectCanvas;
    public GameObject HighScoreCanvas;

    public bool SkipTutorial;
    public GameSetupData gameData;
    public GameObject remoteHSManage;


    void Start()
    {
        gameData = new GameSetupData();
    }

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        //singleton
        if (QS == null)
        {
            //DontDestroyOnLoad(gameObject);
           // QS = this;
        }
        else
        {
            //if (QS != this)
            //{
            //    Destroy(gameObject);
            //}
        }
    }

    public void quitnowPlz()
    {
        Debug.Log("quit");
        Application.Quit();
        EditorApplication.Exit(0);
    }

    public void ShowGameModeSelect()
    {

        Debug.Log("Show GameSelect Canvas");
        GameSelectCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }



    public void GetHighScores()
    {
        HighScoreCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);
    }

    public void HSBackToMenu()
    {
        MainMenuCanvas.SetActive(true);
        HighScoreCanvas.SetActive(false);
    }

    public void SetupStartFile(bool Multiplayer)
    {
        //set game mode
        if (!Multiplayer)
        {
            Debug.Log("singleplayer");
            gameData.setGameMode(gameMode.Singleplayer);
        }
        else
        {
            Debug.Log("multiplayer");
            gameData.setGameMode(gameMode.Multiplayer);
        }

        //set to skip tutorial level if needed
        gameData.setTutorial(SkipTutorial);
        Debug.Log("skip level is set to " + gameData.getTutorial());

        //go to next screen
        CharacterSelectCanvas.SetActive(true);
        GameSelectCanvas.SetActive(false);

    }

    public void SetupCharacter(string selected)
    {
        //set character
        if (selected == "Knight")
        {
            gameData.setCharacter(character.Knight);
        }
        else 
        {
            gameData.setCharacter(character.Ninja);
        }
            Debug.Log("Character selected is " + selected);



        //check if skip tutorial flag is set so game can start
        if (gameData.getTutorial() == false && gameData.getMultiplayer() == gameMode.Singleplayer)
        {
            Debug.Log("start tutorial");
            // start tutorial
            StartLevel(3);
        }

        if (gameData.getMultiplayer() == gameMode.Multiplayer)
        {
            LobbyConnectCanvas.SetActive(true);
            CharacterSelectCanvas.SetActive(false);
        }
        else 
        {
            //go to next screen
            MapSelectCanvas.SetActive(true);
            CharacterSelectCanvas.SetActive(false);
        }

        
    }

    public void SetCutsceneSkip()
    {
        //change boolean on toggle change
        if (!SkipTutorial)
        {
            Debug.Log("skip cut is true");
            SkipTutorial = true;
        }
        else
        {
            Debug.Log("skip cut is false");
            SkipTutorial = false;
        }
    }

    public void StartLevel(int levelNum)
    {
        Debug.Log("char select is " + gameData.getCharacter().ToString());
        remoteHSManage.GetComponent<RemoteHighScoreManager>().charselect(gameData.getCharacter().ToString());

        //destroy save file since starting new game
        if (System.IO.File.Exists("savegame.xml"))
        {
            File.Delete(Application.dataPath + "Assets/Saves/savegame.xml");
            UnityEditor.AssetDatabase.Refresh();
        }
        //MainMenuCanvas.SetActive(true);
        LobbyConnectCanvas.SetActive(false);

        //load approriate scene
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(remoteHSManage);
        SceneManager.LoadScene(levelNum);
    }


    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (remoteHSManage == null)
        {
            remoteHSManage = GameObject.Find("HighScoreController");
        }
    }





}
