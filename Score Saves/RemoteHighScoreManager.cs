using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using SaveLoadS;

[Serializable]
public class HighScoreResult
{
    public int Score;
    public string Name;
    public string code;
    public string message;
}

[Serializable]
public class RootScoreObject
{
    public HighScoreResult[] scores;
}


public class RemoteHighScoreManager : MonoBehaviour
{
    string AorB = null;
    private GameObject charToDisable = null;
    private GameObject camToDisable = null;


    public static RemoteHighScoreManager Instance { get; private set; }

    void Awake()
    {
        // force singleton instance
        //if (Instance == null) { Instance = this; }
        //else { Destroy(gameObject); }
        Instance = this;
        // don't destroy this object when we load scene
        DontDestroyOnLoad(gameObject);
    }



    public void charselect(String character)
    {
        AorB = character;
    }

    public void charhide()
    {
        Debug.Log("AorB is set to " + AorB);

        if (AorB == "Ninja")
        {
            Debug.Log("Show Only Ninja");
            FindObjectOfType<SaveLoadScr>().playerTOSET = GameObject.Find("Ninja");

            // disable knight
            charToDisable = GameObject.Find("paladin");
            camToDisable = GameObject.Find("KnightCamCtrl");
        }
        else if (AorB == "Knight")
        {
            Debug.Log("Show Only Knight");
            FindObjectOfType<SaveLoadScr>().playerTOSET = GameObject.Find("paladin");

            // disable ninja
            charToDisable = GameObject.Find("Ninja");
            camToDisable = GameObject.Find("ninjaCamCtrl");
        }
        else if (AorB == "paladin")
        {
            Debug.Log("Show Only Knight");
            FindObjectOfType<SaveLoadScr>().playerTOSET = GameObject.Find("paladin");

            // disable ninja
            charToDisable = GameObject.Find("Ninja");
            camToDisable = GameObject.Find("ninjaCamCtrl");
        }
        charToDisable.SetActive(false);
        camToDisable.SetActive(false);
    }



    public IEnumerator GetHighScoreCR(Action<RootScoreObject> onCompleteCallback)
    {
        string url = "https://nimblecrime.backendless.app/api/data/GameHighScores?sortBy=%60Score%60%20desc";

        //create a GET UnityWebRequest, passing in our URL
        UnityWebRequest webreq = UnityWebRequest.Get(url);

        // set the request headers as dictated by the backendless documentation (3 headers)
        webreq.SetRequestHeader("application-id", Globals.APPLICATION_ID);
        webreq.SetRequestHeader("secret-key", Globals.REST_SECRET_KEY);
        webreq.SetRequestHeader("application-type", "REST");

        //Send the webrequest and yield (so the script waits until it returns with a result)
        yield return webreq.SendWebRequest();

        //check for webrequest errors
        if (webreq.isNetworkError)
        {
            Debug.Log(webreq.error);
        }
        else
        {
            //Convert the downloadHandler.text property to HighScoreResult (currently JSON)
            RootScoreObject highScoreData = JsonUtility.FromJson<RootScoreObject>("{\"scores\":" + webreq.downloadHandler.text + "}");

            // check that there are no backendless errors
            if (!string.IsNullOrEmpty(highScoreData.scores[1].code))
            {
                Debug.Log("Error:" + highScoreData.scores[1].code + " " + highScoreData.scores[1].message);
            }
            else // call the callback function, passing the score as the parameter
            {
                onCompleteCallback(highScoreData);
            }

        }
    }


    public IEnumerator SetHighScoreCR(string name, int score, Action onCompleteCallback)
    {
        // construct the url for our request, including objectid!
        string url = "https://nimblecrime.backendless.app/api/data/GameHighScores";

        // construct JSON string for data we want to send
        string data = JsonUtility.ToJson(new HighScoreResult { Score = score, Name = name });

        // create PUT UnityWebRequest passing our url and data
        UnityWebRequest webreq = UnityWebRequest.Put(url, data);

        // set the request headers as dictated by the backendless documentation (4 headers)
        webreq.SetRequestHeader("Content-Type", "application/json");
        webreq.SetRequestHeader("application-id", Globals.APPLICATION_ID);
        webreq.SetRequestHeader("secret-key", Globals.REST_SECRET_KEY);
        webreq.SetRequestHeader("application-type", "REST");

        // Send the webrequest and yield (so the script waits until it returns with a result)
        yield return webreq.Send();

        // check for webrequest errors
        if (webreq.isNetworkError)
        {
            Debug.Log(webreq.error);
        }
        else
        {
            //Convert the downloadHandler.text property to HighScoreResult (currently JSON)
            //RootScoreObject highScoreData = JsonUtility.FromJson<RootScoreObject>("{\"scores\":" + webreq.downloadHandler.text + "}");
            HighScoreResult highScoreData = JsonUtility.FromJson<HighScoreResult>(webreq.downloadHandler.text);

            // check that there are no backendless errors
            if (!string.IsNullOrEmpty(highScoreData.code))
            {
                Debug.Log("Error:" + highScoreData.code + " " + highScoreData.message);
            }
            else // call the callback function, passing the score as the parameter
            {
                onCompleteCallback();
            }
        }
    }

}