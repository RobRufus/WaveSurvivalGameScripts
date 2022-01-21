using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leaveTut : MonoBehaviour
{
    public bool unlockFlag = false;
    public GameObject UnlockUIImageA;
    public GameObject UnlockUIImageB;

    private void Update()
    {
        if (unlockFlag)
        {
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
                Debug.Log("leave");
                this.GetComponent<AudioSource>().Play();
                SceneManager.LoadScene(1);
            }
            else 
            {
                Debug.Log("no leave");
            }
        }
    }



    public void setLeaveFlag()
    {
        unlockFlag = true;
    }

}
