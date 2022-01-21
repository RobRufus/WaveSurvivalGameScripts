using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveScr : MonoBehaviour, InteractOnRaycast
{


    public void OnInteract()
    {
        NextWave();
    }



    public void NextWave()
    {
        Debug.Log("next wave");
    }


} 