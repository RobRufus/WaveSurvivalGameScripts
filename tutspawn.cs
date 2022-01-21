using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutspawn : MonoBehaviour
{

    public Transform spawnLoc;
    public GameObject enemie;

    public void spawnTutorialEnemie()
    {
        Instantiate(enemie, spawnLoc.position, spawnLoc.rotation);
    }

}
