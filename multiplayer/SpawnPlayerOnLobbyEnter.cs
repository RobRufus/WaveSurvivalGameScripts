using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerOnLobbyEnter : MonoBehaviour
{

    public int playerCount = 0;
    public GameObject playerSpawnLocationList;
    private Transform[] spawnPoints;

    private void Awake()
    {
        playerCount++;

        spawnPoints = playerSpawnLocationList.GetComponentsInChildren<Transform>();

        if (playerCount == 1)
        {
            //spawn at location 1
            
            PhotonNetwork.Instantiate("MultiplayerPlayerPrefab", spawnPoints[1].position, spawnPoints[1].rotation, 0);
            reHideChar();
        }
        else if (playerCount == 2)
        {
            //spawn at location 2
            PhotonNetwork.Instantiate("MultiplayerPlayerPrefab", spawnPoints[2].position, spawnPoints[2].rotation, 0);
            reHideChar();
        }
        else 
        { 
            Debug.Log("too many players"); 
        }
    }




    private void reHideChar() 
    {
        FindObjectOfType<RemoteHighScoreManager>().charhide();
    }




}
