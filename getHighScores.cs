using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class getHighScores : MonoBehaviour
{

    public Text scoreTexBox;

     
    void Start()
    {
        //StartCoroutine(RemoteHighScoreManager.Instance.GetHighScoreCR());
        StartCoroutine(RemoteHighScoreManager.Instance.GetHighScoreCR(OutputHighScores));
    }

    void OutputHighScores(RootScoreObject highScore)
    {
        //Debug.Log(highScore);

        for (int i = 0; i < highScore.scores.Length; i++)
        {
            scoreTexBox.text += highScore.scores[i].Name + "   :   " + highScore.scores[i].Score + "\n";
        }
    }


}
