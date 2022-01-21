using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLevelParameters : MonoBehaviour
{


    void Start()
    {
        //disable the main menu ui
        GameObject.Find("MainMenuCanvas").GetComponent<Canvas>().enabled = false;
    }
}
