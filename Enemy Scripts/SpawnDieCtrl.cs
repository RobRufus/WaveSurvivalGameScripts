using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnDieCtrl : MonoBehaviour
{

    public bool requireScoreCtrl;
    public ScoreControlerScr scoreControler;
    Rigidbody[] rigidbodies;
    private bool dead = false;


    void Awake()
    {
        scoreControler = FindObjectOfType<ScoreControlerScr>();

        //Update Enemy Count Ui Text
        if (requireScoreCtrl)
        {
            scoreControler.UpdateEnemyCount(+1);
        }
        

        rigidbodies = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = true;
        }
    }

    public void dieOnAxe()
    {
        if (!dead)
        {
            //Increment score
            if (requireScoreCtrl)
            {
                scoreControler.AddScore();
            }

            GetComponent<Animator>().enabled = false;
            for (int i = 0; i < rigidbodies.Length; i++)
            {
                rigidbodies[i].isKinematic = false;
            }

            //Update Enemy Count Ui Text
            if (requireScoreCtrl)
            {
                scoreControler.UpdateEnemyCount(-1);
            }

            //disable Zombie AI to prevent movement
            GetComponent<AgentController>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;

            

            unlockLeave script = this.GetComponent<unlockLeave>();
            if (script != null)
            {
                script.unlockTut();
            }


            //prevent repeat death
            dead = true;
        }
    }


    public void startAIOnSpawn() 
    {
        GetComponent<AgentController>().enabled = true;
    }

}
