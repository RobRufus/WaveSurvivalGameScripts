using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ScoreControlerScr : MonoBehaviour
{
    private int score, enemies = -1, waves = 3;
    public TextMeshProUGUI ScoreTextBox;
    public TextMeshProUGUI EnemyCountTextBox;
    public TextMeshProUGUI WaveCountTextBox; 
    
    public leaveTut leaveLvlObj;
    public GameObject DieUI;
    public GameObject DisablePauseUI;
    public TextMeshProUGUI FinalScoreTextBox;


    private void Update()
    {
        if (ScoreTextBox != null && EnemyCountTextBox != null && WaveCountTextBox != null)
        {
            ScoreTextBox.text = ("Score : " + score);
            EnemyCountTextBox.text = ("Enemies remaining : " + enemies);
            WaveCountTextBox.text = ("Waves remaining : " + waves);
        }

        if (waves == 0 && enemies == 0)
        {
            leaveLvlObj.setLeaveFlag();
        }


        if (SceneManager.GetActiveScene().name != "Multiplayer")
        {
            //if wave finished this wave, update wave data
            if (enemies <= 0 && this.gameObject.GetComponent<SpawnWaveScr>().checkWaveFlag() == true)
            {
                // set wave active flag to false
                this.gameObject.GetComponent<SpawnWaveScr>().setWaveFlag(false);

                //update wave count
                waves -= 1;
                WaveCountTextBox.text = ("Waves remaining : " + waves);
            }
        } else 
        {
            //if wave finished this wave, update wave data
            if (enemies <= 0 && this.gameObject.GetComponent<MultiplayerSpawnWave>().checkWaveFlag() == true)
            {
                // set wave active flag to false
                this.gameObject.GetComponent<MultiplayerSpawnWave>().setWaveFlag(false);

                //update wave count
                waves -= 1;
                WaveCountTextBox.text = ("Waves remaining : " + waves);
            }
        }
    }


    public void SetVarsOnLoad(int Lscore, int Lwaves)
    {
        score = Lscore;
        waves = Lwaves;
    }

    public void AddScore()
    {
        //increment score
        score += 20;
        //set text
        ScoreTextBox.text = ("Score : " + score);
    }

    public void UpdateEnemyCount(int modify)
    {
        enemies += modify;
        EnemyCountTextBox.text = ("Enemies remaining : " + enemies);
    }

    public int getEnemieCount()
    {
        return enemies;
    }

    public int getScore()
    {
        return score;
    }

    public int getWave()
    {
        return waves;
    }

    public void Die()
    {
        // pause and show u die screen
        Debug.Log("die");
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        //show dead UI
        DisablePauseUI.GetComponent<PauseMenuControler>().paused = true;
        DieUI.SetActive(true);

        //set score
        FinalScoreTextBox.text = ("Your Final Score was : " + score);
    }

    public void setVars(int waveNum, int enemys, int scoreSet)
    {
        this.gameObject.GetComponent<SpawnWaveScr>().flag = false;

        score = scoreSet;
        waves = waveNum;
        WaveCountTextBox.text = ("Waves remaining : " + waves);
        enemies = enemys;
    }
    

}

