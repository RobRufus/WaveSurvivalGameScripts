using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leaveLevel : MonoBehaviour
{

    public AudioSource openSound;
    public bool leaveFlag = false;
    public bool unlockFlag = false;
    public int enemies;
    public int unlockvalue = 1;
    public GameObject UnlockUIImageA;
    public GameObject UnlockUIImageB;
    public ScoreControlerScr scoreGet;

    private void Update()
    {
        unlockLeave();
    }


    public void unlockLeave()
    {
        enemies = scoreGet.getEnemieCount();

        if (enemies > unlockvalue)
        {
            leaveFlag = true;
        }


        if (enemies == unlockvalue && leaveFlag == true)
        {
            unlockFlag = true;

            UnlockUIImageA.SetActive(false);
            UnlockUIImageB.SetActive(true);
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            //leave or some shit
            if (unlockFlag == true)
            {
                //leave
                openSound.Play();
                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    SceneManager.LoadScene(0);
                }
                else 
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                
            }
            else
            {
                
            }
        }
    }

}
