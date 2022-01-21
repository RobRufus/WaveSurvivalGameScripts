using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiSpinCtrl : MonoBehaviour
{
    public bool spinFlag;

    // Update is called once per frame
    void Update()
    {
        if (spinFlag)
        {
            this.transform.Rotate(new Vector3(0, 0, 680 * Time.deltaTime));
        }
    }
}
