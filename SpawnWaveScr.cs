using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon;

public class SpawnWaveScr : Photon.MonoBehaviour
{
    public GameObject SpawnPointListParent;
    public GameObject Enemy;
    public GameObject Fog;
    public TextMeshProUGUI UIText;
    public AudioSource bells;
    public bool flag = true;

    private bool waveFlag = false;
    private Transform[] spawnPoints;


    public bool checkWaveFlag()
    {
        return waveFlag;
    }

    public void setWaveFlag(bool tf)
    {
        waveFlag = tf;
    }


    public void WaveControll()
    {
        //check wave flag
        if (this.gameObject.GetComponent<ScoreControlerScr>().getEnemieCount() <= 0 && !waveFlag)
        { 
            waveFlag = true; 
        
            //get children locations into array
            spawnPoints = SpawnPointListParent.GetComponentsInChildren<Transform>();

            //Countdown & Enable Fog
            foreach (Transform location in spawnPoints)
            {
                //spawn at child location
                Instantiate(Fog, location.position, Quaternion.identity);
            }
            StartCoroutine(RoundCountDown());

            //play church bells
            bells.Play();
        }
    }


    public void MPWaveControl() 
    {
        photonView.RPC("MultiplayerWaveControll", PhotonTargets.AllBuffered);
    }


    [PunRPC]
    public void MultiplayerWaveControll()
    {
        //check wave flag
        if (this.gameObject.GetComponent<ScoreControlerScr>().getEnemieCount() <= 0 && !waveFlag)
        {
            waveFlag = true;

            //get children locations into array
            spawnPoints = SpawnPointListParent.GetComponentsInChildren<Transform>();

            //Countdown & Enable Fog
            foreach (Transform location in spawnPoints)
            {
                //spawn at child location
                Instantiate(Fog, location.position, Quaternion.identity);
            }
            StartCoroutine(RoundCountDown());

        }
    }







    public void spawnEnemiesOnLoad(int enemyCount, bool wvFlag)
    {
        if (wvFlag == true)  //if wave is active then pick up spawning where left off
        {
            StartCoroutine(SpawnEnemyWithSkip(enemyCount));
        }
    }


    private IEnumerator SpawnEnemyWithSkip(int skipNum)
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        foreach (Transform location in spawnPoints)
        {

            if (skipNum > 0)
            {
                skipNum--;
            } 
            else
            {
                if (flag == true)
                {


                    //spawn at child location
                    Instantiate(Enemy, location.position, location.rotation);

                    Debug.Log("Spawned Enemy");

                }

                yield return wait;
            }

        }
        setWaveFlag(false);
    }


    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        foreach (Transform location in spawnPoints)
        {
            if (flag == true)
            {
                //spawn at child location
                Instantiate(Enemy, location.position, location.rotation);

                Debug.Log("Spawned Enemy");
            }

            yield return wait;
        }
        setWaveFlag(false);
    }


    private IEnumerator FightText()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        yield return wait;
        UIText.text = "";

    }


    private IEnumerator RoundCountDown()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);
        for (int i = 0; i < 3; i++)
        {
            UIText.text = (3 - i).ToString();
            yield return wait;
        }

        //Wave Start txt
        UIText.text = "Fight!";
        StartCoroutine(FightText());


        //Next Wave
        StartCoroutine(SpawnEnemy());
    }

}
