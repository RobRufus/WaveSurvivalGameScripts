using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipLevel : MonoBehaviour
{
    public GameObject[] enemies;
    public ScoreControlerScr scoreCon;


    public void skipLvl()
    { 
        //find all enemies in scene
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //kill them
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<SpawnDieCtrl>().dieOnAxe();
        }

        scoreCon.setVars(0, 0, 1580);
        
    }






}
