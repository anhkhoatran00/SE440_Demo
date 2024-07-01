using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceNetworkManager : NetworkManager
{
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    SpawnObstacle spawnObstacle;
    [SerializeField]
    private float spawnThreshold = 3f;
    [SerializeField]
    private float countTime = 0f;
    [SerializeField]
    private int maxConn = 1;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        //   base.OnServerAddPlayer(conn);
        Vector3 spawnPoint = spawnPoints[numPlayers].position;

        var player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity) as GameObject;

        NetworkServer.AddPlayerForConnection(conn, player);
    }
    private void FixedUpdate()
    {
        if (!isNetworkActive && numPlayers != maxConn) return;

        countTime += Time.fixedDeltaTime;
        if (countTime >= spawnThreshold)
        {
            countTime -= spawnThreshold;
            spawnObstacle.Spawn();
        }
    }

}
