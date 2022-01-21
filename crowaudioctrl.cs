using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowaudioctrl : MonoBehaviour
{

    public AudioSource sfx;
    private bool playSequential = true;

    // Update is called once per frame
    void Update()
    {
        if (playSequential == true)
        {
            playSequential = false;
            StartCoroutine(playCrowAudio());
        }
    }


    private IEnumerator playCrowAudio()
    {
        WaitForSeconds wait = new WaitForSeconds(Random.Range(10, 30));
        yield return wait;
        sfx.Play();
        playSequential = true;
    }

}
