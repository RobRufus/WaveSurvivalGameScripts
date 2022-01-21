using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreSaveControler : MonoBehaviour
{

    public static ScoreSaveControler Instance;
    public TextMeshProUGUI InputStringBox;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    public void setHighScoreComplete()
    {
        string name = InputStringBox.text;

        GameObject scoreGet = GameObject.Find("EventSystem");
        int score = scoreGet.GetComponent<ScoreControlerScr>().getScore();

        StartCoroutine(RemoteHighScoreManager.Instance.SetHighScoreCR(name, score, BackToMenu));
    }



    public void BackToMenu()
    {
        Time.timeScale = 1;

        Destroy(GameObject.Find("MainMenuCanvas"));
        Destroy(GameObject.Find("HighScoreControler"));

        SceneManager.LoadScene(0);
    }


}