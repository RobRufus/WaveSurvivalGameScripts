using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockLeave : MonoBehaviour
{
    public GameObject leavelvl;

    public void unlockTut()
    {
        leavelvl.GetComponent<leaveTut>().setLeaveFlag();
    }

}
